using Microsoft.AspNetCore.Mvc;
using BackendApi.Repositories;
using BackendApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using BackendApi.Services;

namespace BackendApi.Controllers;

[ApiController]
[Route("api/v1/stores")]
[Produces("application/json")]
public class StoreController : ControllerBase
{
    private readonly IStoreService service;

    public StoreController(IStoreService service) =>
        this.service = service;

    [SwaggerOperation(Summary = "Get all stores.")]
    [HttpGet(Name = "GetAllStores")]
    public async Task<IEnumerable<StoreModel>> GetAll()
    {
        return await service.GetAll();
    }

    [SwaggerOperation(Summary = "Get stores in the specified district.")]
    [HttpGet("districts/{id}", Name = "GetStoresInDistrict")]
    public async Task<IEnumerable<StoreModel>> GetStoresInDistrict(int id)
    {
        return await service.GetAllStoresForDistrict(id);
    }

}