using AddisEats.Api.DTOs;
using AddisEats.Api.Services.RestaurantServices;
using Microsoft.AspNetCore.Mvc;

namespace AddisEats.Api.Controllers;

[ApiController]
[Route("api/v1/restaurants")]
public sealed class RestaurantsController(IRestaurantService restaurants) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(CollectionResponse<RestaurantDto>), StatusCodes.Status200OK)]
    public ActionResult<CollectionResponse<RestaurantDto>> GetAll([FromQuery] RestaurantQuery query) =>
        Ok(new CollectionResponse<RestaurantDto>(restaurants.GetAll(query)));

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RestaurantDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<RestaurantDto> GetOne(int id) =>
        restaurants.GetById(id) is { } restaurant ? Ok(restaurant) : NotFound();

    [HttpGet("by-name/{name}")]
    [ProducesResponseType(typeof(RestaurantDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<RestaurantDto> GetByName(string name) =>
        restaurants.GetByName(name) is { } restaurant ? Ok(restaurant) : NotFound();
}
