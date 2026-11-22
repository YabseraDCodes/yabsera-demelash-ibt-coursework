using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AddisEats.Api.Controllers;

[ApiController]
[Route("api/v1/delivery-fees")]
public sealed class DeliveryController(IOptions<DeliveryOptions> options) : ControllerBase
{
    [HttpGet]
    public IActionResult GetFees() => Ok(new { options.Value.FreeAboveEtb, options.Value.DefaultFeeEtb });
}
