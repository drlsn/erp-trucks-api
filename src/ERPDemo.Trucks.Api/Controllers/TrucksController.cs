using ERPDemo.Trucks.Api.ApiContracts;
using ERPDemo.Trucks.Api.Entities;
using ERPDemo.Trucks.Api.Extensions;
using ERPDemo.Trucks.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ERPDemo.Trucks.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TrucksController : ControllerBase
{
    private readonly ITruckRepository _truckRepository;
    private readonly GetTrucksQueryHandler _getTrucksQueryHandler;

    public TrucksController(
        ITruckRepository truckRepository,
        GetTrucksQueryHandler getTrucksQueryHandler)
    {
        _truckRepository = truckRepository;
        _getTrucksQueryHandler = getTrucksQueryHandler;
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> Get([FromRoute] GetTruckApiQuery query)
    {
        var truck = await _truckRepository.Get(new TruckCode(query.Code));
        if (truck is null)
            return NotFound();

        return Ok(truck);
    }

    [HttpGet]
    public async Task<IActionResult> GetMany([FromQuery] GetTrucksApiQuery query)
    {
        var response = await _getTrucksQueryHandler.Handle(new GetTrucksQuery(
            query.Sort_By,
            query.Status,
            query.Name));

        return response is not null ?
            Ok(response.Trucks) :
            NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTruckApiCommand command)
    {
        var truck = new Truck(
            new TruckCode(command.Code),
            command.Name,
            command.Description);

        return await _truckRepository.Add(truck) ?
            Created($"/trucks/{command.Code}", null) :
            NotFound("Could not create the resource");
    }

    [HttpPatch("{Code}")]
    public async Task<IActionResult> Update(
        [FromRoute] string code, [FromBody] UpdateTruckApiCommand command)
    {
        var truck = await _truckRepository.Get(new TruckCode(code));
        if (truck is null)
            return NotFound();

        if (command.Status.IsNullOrEmpty() &&
            command.Name.IsNullOrEmpty() &&
            command.Description.IsNullOrEmpty())
        {
            return NotFound();
        }

        if (!command.Status.IsNullOrEmpty())
        {
            var result = truck.UpdateStatus(new TruckStatus(command.Status));
            if (!result.IsSuccess)
                return BadRequest(result.Message);
        }

        if (!command.Name.IsNullOrEmpty())
            truck.Name = command.Name;

        if (!command.Description.IsNullOrEmpty())
            truck.Description = command.Description;

        return await _truckRepository.Update(truck) ?
            Ok() :
            NotFound("Could not update the resource");
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteTruckApiCommand command)
    {
        return await _truckRepository.Delete(new TruckCode(command.Code)) ?
             NoContent() :
             NotFound("Could not delete the resource");
    }
}
