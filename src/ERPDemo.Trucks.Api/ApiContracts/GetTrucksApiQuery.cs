namespace ERPDemo.Trucks.Api.ApiContracts;

public class GetTrucksApiQuery
{
    public string? Sort_By { get; set; }
    public string? Status { get; set; }
    public string? Name { get; set; }
}
