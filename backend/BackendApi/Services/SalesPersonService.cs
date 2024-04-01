using BackendApi.Models;
using BackendApi.Repositories;

namespace BackendApi.Services;

public interface ISalesPersonService
{
    Task<IEnumerable<SalesPersonModel>> GetAll();
    Task<IEnumerable<SalesPersonModel>> GetSalesPersonsFromDistrict(int id);
    Task AddSalesPerson(AddSalesPersonRequestModel req, int districtId);
    Task RemoveSalesPerson(int districtId, int salespersonId);
}

public class SalesPersonService : ISalesPersonService
{
    private readonly ISalesPersonRepository salesPersonRepository;
    private readonly IDistrictRepository districtRepository;
    private readonly IDistrictSalesPersonRepository districtSalesPersonRepository;


    public SalesPersonService(ISalesPersonRepository salesPersonRepository,
                              IDistrictRepository districtRepository,
                              IDistrictSalesPersonRepository districtSalesPersonRepository)
    {
        this.salesPersonRepository = salesPersonRepository;
        this.districtRepository = districtRepository;
        this.districtSalesPersonRepository = districtSalesPersonRepository;
    }

    public async Task<IEnumerable<SalesPersonModel>> GetAll()
    {
        var entities = await salesPersonRepository.GetAll();

        return entities.Select(e => new SalesPersonModel(e.id, e.name, e.is_primary));
    }

    public async Task<IEnumerable<SalesPersonModel>> GetSalesPersonsFromDistrict(int id)
    {
        var entities = await salesPersonRepository.GetAllByDistrictId(id);

        return entities.Select(e => new SalesPersonModel(e.id, e.name, e.is_primary));
    }

    public async Task AddSalesPerson(AddSalesPersonRequestModel req, int district_id)
    {
        if (req.isPrimary)
        {
            await districtRepository.AddPrimarySalesPerson(district_id, req.salesPersonId);
        }
        else
        {
            await districtSalesPersonRepository.AddSecondarySalesPerson(district_id, req.salesPersonId);
        }
    }

    public async Task RemoveSalesPerson(int districtId, int salespersonId)
    {
        await districtSalesPersonRepository.RemoveSecondarySalesPerson(districtId, salespersonId);
    }

}