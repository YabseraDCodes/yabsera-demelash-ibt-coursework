using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AddisEats.Api.Controllers;

[ApiController]
[Route("api/v1/delivery")]
public sealed class DeliveryController(IOptions<DeliveryOptions> options) : ControllerBase
{
    [HttpGet("fees")]
    public IActionResult GetFees() => Ok(new { options.Value.FreeAboveEtb, options.Value.DefaultFeeEtb });
}
