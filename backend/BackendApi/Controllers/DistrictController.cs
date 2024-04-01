using Microsoft.AspNetCore.Mvc;
using BackendApi.Services;
using BackendApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace BackendApi.Controllers;

[ApiController]
[Route("api/v1/districts")]
[Produces("application/json")]
public class DistrictController : ControllerBase
{
    private readonly IDistrictService service;

    public DistrictController(IDistrictService service) =>
        this.service = service;

    [SwaggerOperation(Summary = "Get all districts.")]
    [HttpGet(Name = "GetAllDistricts")]
    public async Task<IEnumerable<DistrictModel>> GetAll()
    {
        return await service.GetAll();
    }

    [SwaggerOperation(Summary = "Get specified district.")]
    [HttpGet("{id}", Name = "GetDistrictById")]
    public async Task<DistrictModel> Get(int id)
    {
        return await service.GetById(id);
    }

}