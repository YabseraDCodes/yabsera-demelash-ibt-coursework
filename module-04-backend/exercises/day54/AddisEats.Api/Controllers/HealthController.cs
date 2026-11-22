using AddisEats.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AddisEats.Api.Controllers;

[ApiController]
[Route("api/v1/health")]
public sealed class HealthController(IClock clock, IWebHostEnvironment environment) : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new { utcNow = clock.UtcNow, environment = environment.EnvironmentName });
}
