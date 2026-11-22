using AddisEats.Api.Models;
using AddisEats.Api.DTOs;
using AddisEats.Api.Services.MenuServices;
using Microsoft.AspNetCore.Mvc;

namespace AddisEats.Api.Controllers;

[ApiController]
[Route("api/v1/menu")]
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
            return NotFound();
        }

        return Ok(menuItem);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MenuItem), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<MenuItem> Create([FromBody] MenuItem menuItem)
    {
        var createdItem = _menuService.Create(menuItem);
        return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
    }
}
