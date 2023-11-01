using Microsoft.AspNetCore.Mvc;

namespace ERPDemo.Trucks.Api.ApiContracts;

public class GetTruckApiQuery
{
    [FromRoute]
    public required string Code { get; init; }
}
