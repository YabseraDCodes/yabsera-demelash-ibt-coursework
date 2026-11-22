using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddisEats.Api.Controllers;

[ApiController]
[Route("api/v1/protected-resources")]
public sealed class ProtectedController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public IActionResult Get() => Ok(new { message = "Authorized" });
}
