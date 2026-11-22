using AddisEats.Api.DTOs;
using AddisEats.Api.Models;
using AddisEats.Api.Services.RestaurantServices;
using Microsoft.AspNetCore.Mvc;

namespace AddisEats.Api.Controllers;

[ApiController]
[Route("api/v1/restaurants")]
public class RestaurantsController(IRestaurantService restaurants) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<RestaurantDTO>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<RestaurantDTO>> GetAll(
        [FromQuery] string? area, [FromQuery] int page = 1, [FromQuery] int limit = 10)
    {
        if (page < 1 || limit < 1 || limit > 100) return BadRequest("page must be positive and limit must be between 1 and 100.");
        return Ok(restaurants.GetAll(area, page, limit));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RestaurantDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<RestaurantDTO> GetById(int id) =>
        restaurants.GetById(id) is { } restaurant ? Ok(restaurant) : NotFound();

    [HttpGet("{id:int}/menu")]
    [ProducesResponseType(typeof(IReadOnlyList<MenuItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IReadOnlyList<MenuItem>> GetMenu(int id)
    {
        var menu = restaurants.GetMenu(id);
        return menu is null ? NotFound() : Ok(menu);
    }
}
