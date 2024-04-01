namespace BackendApi.Entities;

public class SalesPersonEntity
{
    public required int id { get; set; }

    public required string name { get; set; }

    public bool is_primary { get; set; }

}