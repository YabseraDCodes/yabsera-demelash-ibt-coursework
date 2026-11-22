using AddisEats.Api.Models;
using AddisEats.Api.DTOs;
using AddisEats.Api.Services.MenuServices;
using Microsoft.AspNetCore.Mvc;

namespace AddisEats.Api.Controllers;

[ApiController]
[Route("api/v1/menu-items")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;
    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(CollectionResponse<MenuItem>), StatusCodes.Status200OK)]
    public ActionResult<CollectionResponse<MenuItem>> GetAll([FromQuery] string? category)
    {
        return Ok(new CollectionResponse<MenuItem>(_menuService.GetAll(category)));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MenuItem), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MenuItem> GetById(int id)
    {
        var menuItem = _menuService.GetById(id);
        if (menuItem == null)
        {
            return Problem(statusCode: 404, title: "Menu item not found.", detail: $"Menu item {id} was not found.", extensions: new Dictionary<string, object?> { ["code"] = "menu_item_not_found" });
        }

        return Ok(menuItem);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MenuItem), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public ActionResult<MenuItem> Create([FromBody] MenuItem menuItem)
    {
        if (string.IsNullOrWhiteSpace(menuItem.Name) || string.IsNullOrWhiteSpace(menuItem.Category) || menuItem.Price <= 0)
            return Problem(statusCode: 400, title: "Menu item is not valid.", detail: "Name, category, and a price above zero are required.", extensions: new Dictionary<string, object?> { ["code"] = "invalid_menu_item" });
        var createdItem = _menuService.Create(menuItem);
        return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
    }

}
