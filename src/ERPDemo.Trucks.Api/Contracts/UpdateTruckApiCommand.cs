using Microsoft.AspNetCore.Mvc;

namespace ERPDemo.Trucks.Api.Contracts;

public class UpdateTruckApiCommand
{
    [FromRoute]
    public required string Code { get; init; }
    public string? Status { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
}
