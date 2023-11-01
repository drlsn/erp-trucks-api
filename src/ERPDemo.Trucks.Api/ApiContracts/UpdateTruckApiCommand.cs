namespace ERPDemo.Trucks.Api.ApiContracts;

public class UpdateTruckApiCommand
{
    public string? Status { get; init; }

    public string? Name { get; init; }

    public string? Description { get; init; }
}
