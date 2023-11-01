using Microsoft.AspNetCore.Mvc;

namespace ERPDemo.Trucks.Api.ApiContracts;

public class DeleteTruckApiCommand
{
    [FromRoute]
    public required string Code { get; init; }
}
