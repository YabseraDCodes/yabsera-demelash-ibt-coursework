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
    public ActionResult<CollectionResponse<Order>> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
    {
        if (page < 1 || limit < 1 || limit > 100 || (long)(page - 1) * limit > int.MaxValue) return Problem(statusCode: 400, title: "Page or limit is not valid.", detail: "Page must be at least 1 and limit must be between 1 and 100.", extensions: new Dictionary<string, object?> { ["code"] = "invalid_pagination" });
        return Ok(new CollectionResponse<Order>(orders.GetAll().Skip((page - 1) * limit).Take(limit).ToList()));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Order> GetById(int id) => orders.GetById(id) is { } order ? Ok(order) : Problem(statusCode: 404, title: "Order not found.", detail: $"Order {id} was not found.", extensions: new Dictionary<string, object?> { ["code"] = "order_not_found" });

    [HttpPost]
    [ProducesResponseType(typeof(Order), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Order> Create([FromBody] CreateOrderRequest request)
    {
        if (request.RestaurantId < 1 || request.MenuItemIds is null || request.MenuItemIds.Count == 0 || request.MenuItemIds.Any(id => id < 1))
            return Problem(statusCode: 400, title: "Order is not valid.", detail: "Use a positive restaurant ID and at least one positive menu item ID.", extensions: new Dictionary<string, object?> { ["code"] = "invalid_order" });
        var created = orders.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPatch("{id:int}/statuses")]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Order> UpdateStatus(int id, [FromBody] UpdateOrderStatusRequest request)
    {
        if (!Enum.IsDefined(request.Status)) return Problem(statusCode: 400, title: "Order status is not valid.", detail: "Choose a supported order status.", extensions: new Dictionary<string, object?> { ["code"] = "invalid_order_status" });
        var updated = orders.UpdateStatus(id, request.Status);
        return updated is null ? Problem(statusCode: 404, title: "Order not found.", detail: $"Order {id} was not found.", extensions: new Dictionary<string, object?> { ["code"] = "order_not_found" }) : NoContent();
    }
}
