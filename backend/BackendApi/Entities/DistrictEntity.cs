namespace BackendApi.Entities;

public class DistrictEntity
{
    public required int id { get; set; }

    public required string name { get; set; }

    public required int primary_salesperson_id { get; set; }

}