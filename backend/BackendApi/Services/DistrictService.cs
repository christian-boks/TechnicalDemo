using BackendApi.Models;
using BackendApi.Repositories;

namespace BackendApi.Services;

public interface IDistrictService
{
    Task<IEnumerable<DistrictModel>> GetAll();
    Task<DistrictModel> GetById(int id);
}

public class DistrictService : IDistrictService
{
    private readonly IDistrictRepository districtRepository;
    private readonly IDistrictSalesPersonRepository districtSalesPersonRepository;

    public DistrictService(IDistrictRepository districtRepository, IDistrictSalesPersonRepository districtSalesPersonRepository)
    {
        this.districtRepository = districtRepository;
        this.districtSalesPersonRepository = districtSalesPersonRepository;
    }

    public async Task<IEnumerable<DistrictModel>> GetAll()
    {
        var entities = await districtRepository.GetAll();
        return entities.Select(e => new DistrictModel(e.id, e.name));
    }

    public async Task<DistrictModel> GetById(int id)
    {
        var entity = await districtRepository.GetById(id);
        return new DistrictModel(entity.id, entity.name);
    }

}