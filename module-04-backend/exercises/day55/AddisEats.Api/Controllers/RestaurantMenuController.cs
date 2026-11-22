using AddisEats.Api.Models;
using AddisEats.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AddisEats.Api.Controllers;

[ApiController]
[Route("api/v1/restaurants")]
public class RestaurantMenuController(IRestaurantService restaurantService) : ControllerBase
{
    [HttpGet("{id:int}/menu")]
    [ProducesResponseType(typeof(IReadOnlyList<MenuItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IReadOnlyList<MenuItem>> GetMenu(int id)
    {
        var menu = restaurantService.GetMenu(id);
        return menu is null ? NotFound() : Ok(menu);
    }
}
