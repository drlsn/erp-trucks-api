using Microsoft.AspNetCore.Mvc;

namespace ERPDemo.Trucks.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TruckController : ControllerBase
{
    [HttpGet]
    public async Task<IAsyncResult> Get()
    {
        return null;
    }
}
