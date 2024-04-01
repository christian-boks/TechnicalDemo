namespace BackendApi.Models;

public class SalesPersonModel
{
    public int id { get; set; }

    public string name { get; set; }

    public bool isPrimary { get; set; }

    public SalesPersonModel(int id, string name, bool isPrimary)
    {
        this.id = id;
        this.name = name;
        this.isPrimary = isPrimary;
    }
}