using System.Net;
using System.Security.Permissions;
using System.Text.Json;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Utils;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        Guid requestId = Guid.NewGuid();
        try
        {
            await _next(httpContext);
        }
        catch (NotFoundException ex)
        {
            _logger.LogError("Caught an exception: {0}, requestId: {1}", ex.GetType(), requestId);

            var em = new ErrorMessage
            {
                statusCode = (int)HttpStatusCode.NotFound,
                message = "Not found",
                requestId = requestId.ToString()
            };

            await HandleExceptionAsync(httpContext, em);
        }
        catch (AlreadyExistsException ex)
        {
            _logger.LogError("Caught an exception: {0}, requestId: {1}", ex.GetType(), requestId);

            var em = new ErrorMessage
            {
                statusCode = (int)HttpStatusCode.Conflict,
                message = "Already exists",
                requestId = requestId.ToString()
            };

            await HandleExceptionAsync(httpContext, em);
        }
        catch (Exception ex)
        {
            _logger.LogError("Caught an exception: {0}, requestId: {1}", ex.GetType(), requestId);

            var em = new ErrorMessage
            {
                statusCode = (int)HttpStatusCode.InternalServerError,
                message = "Operation failed",
                requestId = requestId.ToString()
            };

            await HandleExceptionAsync(httpContext, em);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, ErrorMessage errorMessage)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = errorMessage.statusCode;

        await context.Response.WriteAsync(errorMessage.ToString());
    }
}

public class GrpcGlobalExceptionHandlerInterceptor : Interceptor
{
    private readonly ILogger<GrpcGlobalExceptionHandlerInterceptor> _logger;

    public GrpcGlobalExceptionHandlerInterceptor(ILogger<GrpcGlobalExceptionHandlerInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        Guid requestId = Guid.NewGuid();

        try
        {
            return await base.UnaryServerHandler(request, context, continuation);
        }
        catch (NotFoundException ex)
        {
            _logger.LogError("Caught an exception: {0}, requestId: {1}", ex.GetType(), requestId);

            throw new RpcException(new Status(StatusCode.NotFound, "The Entity Not Found. Request Id: " + requestId));
        }
        catch (Exception e)
        {
            _logger.LogError("Caught an exception: {0}, requestId: {1}", e.GetType(), requestId);

            throw new RpcException(new Status(StatusCode.Internal, "Server error. Request Id: " + requestId));
        }
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureCustomExceptionMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}

public class GrpcErrorMessage
{
    public int Code { get; set; }
    public string Message { get; set; } = null!;
}

public class ErrorMessage
{
    public int statusCode { get; set; }
    public string message { get; set; } = null!;
    public string requestId { get; set; } = null!;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}