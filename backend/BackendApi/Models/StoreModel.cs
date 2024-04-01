namespace BackendApi.Models;

public class StoreModel
{
    public int id { get; set; }

    public string city { get; set; }

    public int districtId { get; set; }

    public StoreModel(int id, string city, int districtId)
    {
        this.id = id;
        this.city = city;
        this.districtId = districtId;
    }
}