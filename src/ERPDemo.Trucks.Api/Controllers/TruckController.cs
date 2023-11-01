using ERPDemo.Trucks.Api.Contracts;
using ERPDemo.Trucks.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ERPDemo.Trucks.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TruckController : ControllerBase
{
    private readonly ITruckRepository _truckRepository;

    public TruckController(ITruckRepository truckRepository) =>
        _truckRepository = truckRepository;

    [HttpGet]
    public async Task<IActionResult> Get(GetTruckApiQuery query)
    {
        var truck = await _truckRepository.Get(new TruckCode(query.Code));
        if (truck is null)
            return NotFound();

        return Ok(truck);
    }

    [HttpGet]
    public async Task<IActionResult> GetMany()
    {
        return null;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTruckApiCommand command)
    {
        var truck = new Truck(
            new TruckCode(command.Code),
            new TruckStatus(command.Status),
            command.Name,
            command.Description);

        return await _truckRepository.Add(truck) ?
            Created($"/trucks/{command.Code}", truck) :
            NotFound("Could not create the resource");
    }

    [HttpPatch]
    public async Task<IActionResult> Update(UpdateTruckApiCommand command)
    {
        var truck = await _truckRepository.Get(new TruckCode(command.Code));
        if (truck is null)
            return NotFound();

        if (command.Status is not null or [])
        {
            var result = truck.UpdateStatus(truck.Status);
            if (!result.IsSuccess)
                return BadRequest(result.Message);
        }

        if (command.Name is not null or [])
            truck.Name = command.Name;

        if (command.Description is not null or [])
            truck.Description = command.Description;

        return await _truckRepository.Update(truck) ?
            Ok() :
            NotFound("Could not update the resource");
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteTruckApiCommand command)
    {
        return await _truckRepository.Delete(new TruckCode(command.Code)) ?
             NoContent() :
             NotFound("Could not delete the resource");
    }
}
