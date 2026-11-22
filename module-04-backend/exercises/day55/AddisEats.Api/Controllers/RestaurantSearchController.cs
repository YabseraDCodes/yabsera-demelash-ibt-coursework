using AddisEats.Api.DTOs;
using AddisEats.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AddisEats.Api.Controllers;

[ApiController]
[Route("api/v1/restaurants")]
public class RestaurantSearchController(IRestaurantService restaurantService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<RestaurantDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<RestaurantDto>> Search(
        [FromQuery] string? area, [FromQuery] int page = 1, [FromQuery] int limit = 10)
    {
        if (page < 1 || limit < 1 || limit > 100)
        {
            return BadRequest("Page must be at least 1 and limit must be between 1 and 100.");
        }

        return Ok(restaurantService.Search(area, page, limit));
    }
}
