namespace ERPDemo.Trucks.Api.ApiContracts;

public class CreateTruckApiCommand
{
    public required string Code { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}
