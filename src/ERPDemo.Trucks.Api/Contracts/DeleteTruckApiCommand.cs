using Microsoft.AspNetCore.Mvc;

namespace ERPDemo.Trucks.Api.Contracts;

public class DeleteTruckApiCommand
{
    [FromRoute]
    public required string Code { get; init; }
}
