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
    [ProducesResponseType(typeof(CollectionResponse<RestaurantDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CollectionResponse<RestaurantDTO>> GetAll([FromQuery] RestaurantQuery query)
    {
        var page = query.Page ?? 1;
        var limit = query.Limit ?? 10;
        if (page < 1 || limit < 1 || limit > 100) return BadRequest("page must be positive and limit must be between 1 and 100.");
        return Ok(new CollectionResponse<RestaurantDTO>(restaurants.GetAll(query)));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RestaurantDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<RestaurantDTO> GetById(int id) =>
        restaurants.GetById(id) is { } restaurant ? Ok(restaurant) : NotFound();

    [HttpGet("{id:int}/menu")]
    [ProducesResponseType(typeof(CollectionResponse<MenuItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<CollectionResponse<MenuItem>> GetMenu(int id)
    {
        var menu = restaurants.GetMenu(id);
        return menu is null ? NotFound() : Ok(new CollectionResponse<MenuItem>(menu));
    }
}
