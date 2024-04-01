using BackendApi.Models;
using BackendApi.Repositories;

namespace BackendApi.Services;

public interface IStoreService
{
    Task<IEnumerable<StoreModel>> GetAll();
    Task<IEnumerable<StoreModel>> GetAllStoresForDistrict(int id);
}

public class StoreService : IStoreService
{
    private readonly IStoreRepository storeRepository;

    public StoreService(IStoreRepository storeRepository)
    {
        this.storeRepository = storeRepository;
    }

    public async Task<IEnumerable<StoreModel>> GetAll()
    {
        var entities = await storeRepository.GetAll();
        return entities.Select(e => new StoreModel(e.id, e.city, e.district_id));
    }

    public async Task<IEnumerable<StoreModel>> GetAllStoresForDistrict(int id)
    {
        var entities = await storeRepository.GetAllStoresForDistrict(id);
        return entities.Select(e => new StoreModel(e.id, e.city, e.district_id));
    }
}