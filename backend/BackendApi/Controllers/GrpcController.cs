using BackendApi.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
namespace BackendApi.Controllers;

public class GrpcController : BackendApi.BackendApiBase
{
    private readonly IDistrictService districtService;
    private readonly ISalesPersonService salesPersonService;
    private readonly IStoreService storeService;

    public GrpcController(IDistrictService districtService, ISalesPersonService salesPersonService, IStoreService storeService)
    {
        this.districtService = districtService;
        this.salesPersonService = salesPersonService;
        this.storeService = storeService;
    }

    public override async Task<GetAllDistrictsReply> GetAllDistricts(Empty request, ServerCallContext context)
    {
        var reply = new GetAllDistrictsReply();
        var list = await districtService.GetAll();
        reply.List.Add(list.Select(e => new District { Name = e.name, Id = e.id }));
        return reply;
    }

    public override async Task<GetAllSalesPersonsForDistrictReply> GetAllSalesPersonsForDistrict(GetAllSalesPersonsForDistrictRequest request, ServerCallContext context)
    {
        var salesPersons = await salesPersonService.GetSalesPersonsFromDistrict(request.DistrictId);
        var reply = new GetAllSalesPersonsForDistrictReply();
        reply.List.Add(salesPersons.Select(e => new SalesPerson { Name = e.name, Id = e.id, IsPrimary = e.isPrimary }));
        return reply;
    }

    public override async Task<Empty> AddSalesPersonToDistrict(AddSalesPersonToDistrictRequest request, ServerCallContext context)
    {
        await salesPersonService.AddSalesPerson(new Models.AddSalesPersonRequestModel { isPrimary = request.IsPrimary, salesPersonId = request.SalesPersonId }, request.DistrictId);
        return new Empty { };
    }

    public override async Task<Empty> RemoveSalesPersonFromDistrict(RemoveSalesPersonFromDistrictRequest request, ServerCallContext context)
    {
        await salesPersonService.RemoveSalesPerson(request.DistrictId, request.SalesPersonId);
        return new Empty { };
    }

    public override async Task<GetAllStoresForDistrictReply> GetAllStoresForDistrict(GetAllStoresForDistrictRequest request, ServerCallContext context)
    {
        var stores = await storeService.GetAllStoresForDistrict(request.DistrictId);
        var reply = new GetAllStoresForDistrictReply();
        reply.List.Add(stores.Select(e => new Store { Id = e.id, DistrictId = e.districtId, City = e.city }));
        return reply;
    }
}