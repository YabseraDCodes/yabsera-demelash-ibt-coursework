using AddisEats.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AddisEats.Api.Controllers;

[ApiController]
[Route("api/v1/payment-providers")]
public sealed class PaymentsController(IPaymentProvider provider) : ControllerBase
{
    [HttpGet]
    public IActionResult GetProvider() => Ok(new { provider = provider.Name });
}
