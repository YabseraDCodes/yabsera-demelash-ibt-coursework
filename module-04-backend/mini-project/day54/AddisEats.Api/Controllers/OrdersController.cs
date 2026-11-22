using AddisEats.Api.DTOs;
using AddisEats.Api.Models;
using AddisEats.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AddisEats.Api.Controllers;

[ApiController]
[Route("api/v1/orders")]
public class OrdersController(IOrderService orders) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(CollectionResponse<Order>), StatusCodes.Status200OK)]
    public ActionResult<CollectionResponse<Order>> GetAll() => Ok(new CollectionResponse<Order>(orders.GetAll()));

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Order> GetById(int id) => orders.GetById(id) is { } order ? Ok(order) : NotFound();

    [HttpPost]
    [ProducesResponseType(typeof(Order), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Order> Create([FromBody] CreateOrderRequest request)
    {
        if (request.RestaurantId < 1 || request.MenuItemIds.Count == 0 || request.MenuItemIds.Any(id => id < 1))
            return BadRequest("A positive restaurant ID and at least one positive menu item ID are required.");
        var created = orders.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPatch("{id:int}/status")]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Order> UpdateStatus(int id, [FromBody] UpdateOrderStatusRequest request)
    {
        if (!Enum.IsDefined(request.Status)) return BadRequest("Unknown order status.");
        var updated = orders.UpdateStatus(id, request.Status);
        return updated is null ? NotFound() : Ok(updated);
    }
}
