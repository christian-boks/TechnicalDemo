namespace BackendApi.Models;

public class DistrictModel
{
    public int id { get; set; }

    public string name { get; set; }

    public DistrictModel(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}