using Microsoft.AspNetCore.Mvc;

namespace ERPDemo.Trucks.Api.Contracts;

public class GetTruckApiQuery
{
    [FromRoute]
    public required string Code { get; init; }
}
