using BackendApi.Utils;
using BackendApi.Repositories;
using BackendApi.Services;
using Microsoft.OpenApi.Models;
using Serilog;
using BackendApi.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc(c => c.Interceptors.Add<GrpcGlobalExceptionHandlerInterceptor>());

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
builder.Services.AddSingleton<IDistrictSalesPersonRepository, DistrictSalesPersonRepository>();
builder.Services.AddSingleton<IDistrictRepository, DistrictRepository>();
builder.Services.AddSingleton<IDistrictService, DistrictService>();
builder.Services.AddSingleton<ISalesPersonRepository, SalesPersonRepository>();
builder.Services.AddSingleton<ISalesPersonService, SalesPersonService>();
builder.Services.AddSingleton<IStoreRepository, StoreRepository>();
builder.Services.AddSingleton<IStoreService, StoreService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.AddServer(new OpenApiServer { Url = "http://localhost:5278" });
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackendApi", Version = "v1" });
});


var app = builder.Build();

// Wrap any thrown exceptions in a fitting json object that can be returned to the client
app.ConfigureCustomExceptionMiddleware();

// Do we want to display this in production? Are these APIs for the customer or developers testing?
// If they are for the developers, they should be hidden behind some permissions so only admins can see them
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.MapGrpcService<GrpcController>();

// Be sure to set these up correctly if running as a real application, not just a test like this
app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials()
            );

app.Run();

