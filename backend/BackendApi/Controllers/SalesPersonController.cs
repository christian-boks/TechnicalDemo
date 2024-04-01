using Microsoft.AspNetCore.Mvc;
using BackendApi.Repositories;
using BackendApi.Models;
using BackendApi.Services;
using Swashbuckle.AspNetCore.Annotations;
using BackendApi.Utils;

namespace BackendApi.Controllers;

[ApiController]
[Route("api/v1/salespersons")]
[Produces("application/json")]
[Consumes("application/json")]
public class SalesPersonController : ControllerBase
{
    private readonly ISalesPersonService salesPersonService;

    public SalesPersonController(ISalesPersonService service) =>
        this.salesPersonService = service;

    [SwaggerOperation(Summary = "Get all sales persons.")]
    [HttpGet(Name = "GetAllSalesPersons")]
    public async Task<IEnumerable<SalesPersonModel>> GetAll()
    {
        return await salesPersonService.GetAll();
    }

    [SwaggerOperation(Summary = "Get all sales persons from the specified district.")]
    [HttpGet("districts/{id}", Name = "GetSalesPersonByDistrictId")]
    public async Task<IEnumerable<SalesPersonModel>> GetById(int id)
    {
        return await salesPersonService.GetSalesPersonsFromDistrict(id);
    }

    [SwaggerOperation(Summary = "Add sales person to the specified district.")]
    [HttpPost("districts/{id}", Name = "AddSalesPersonToDistrict")]
    [SwaggerResponse(200, "Sales person added")]
    [SwaggerResponse(404, "Sales person not found", typeof(ErrorMessage))]
    [SwaggerResponse(409, "Sales person already present", typeof(ErrorMessage))]
    [SwaggerResponse(500, "Operation failed", typeof(ErrorMessage))]
    public async Task<IActionResult> AddSalesPerson(int id, AddSalesPersonRequestModel req)
    {
        await salesPersonService.AddSalesPerson(req, id);
        return Ok();
    }

    [SwaggerOperation(Summary = "Remove secondary sales person from district.")]
    [HttpDelete("{salespersonId}/districts/{districtId}", Name = "RemoveSalesPersonFromDistrict")]
    [SwaggerResponse(200, "Sales person removed")]
    [SwaggerResponse(404, "Sales person not found", typeof(ErrorMessage))]
    [SwaggerResponse(500, "Operation failed", typeof(ErrorMessage))]
    public async Task<IActionResult> RemoveSalesPerson(int districtId, int salespersonId)
    {
        await salesPersonService.RemoveSalesPerson(districtId, salespersonId);
        return Ok();
    }

}