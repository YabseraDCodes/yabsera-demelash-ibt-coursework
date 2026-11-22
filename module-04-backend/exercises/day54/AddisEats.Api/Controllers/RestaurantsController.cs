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
    public ActionResult<CollectionResponse<RestaurantDto>> GetAll([FromQuery] RestaurantQuery query)
    {
        var page = query.Page ?? 1;
        var limit = query.Limit ?? 10;
        if (query.MaxFeeEtb < 0)
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest,
                title: "The delivery fee is not valid.",
                detail: "maxFeeEtb cannot be negative.",
                extensions: new Dictionary<string, object?> { ["code"] = "invalid_money_amount" });
        }

        if (page < 1 || limit < 1 || limit > 100 || (long)(page - 1) * limit > int.MaxValue)
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest,
                title: "A search value is outside its allowed range.",
                detail: "Page must be at least 1, limit must be from 1 to 100, and maxFeeEtb cannot be negative.",
                extensions: new Dictionary<string, object?> { ["code"] = "invalid_pagination" });
        }

        return Ok(new CollectionResponse<RestaurantDto>(restaurants.GetAll(query)));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RestaurantDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<RestaurantDto> GetOne(int id) =>
        restaurants.GetById(id) is { } restaurant ? Ok(restaurant) : NotFound();

}
