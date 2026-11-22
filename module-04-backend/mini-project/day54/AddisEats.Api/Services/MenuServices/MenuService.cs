using AddisEats.Api.Models;
using Microsoft.Extensions.Options;

namespace AddisEats.Api.Services.MenuServices;

public class MenuService : IMenuService
{
    private static readonly List<MenuItem> _menuItems =
    [
        new() { Id = 1, Name = "Doro Wot", Category = "Main", Price = 450m },
        new() { Id = 2, Name = "Tibs", Category = "Main", Price = 520m },
        new() { Id = 3, Name = "Sambusa", Category = "Starter", Price = 80m },
        new() { Id = 4, Name = "Chechebsa", Category = "Breakfast", Price = 180m },
        new() { Id = 5, Name = "Shiro", Category = "Main", Price = 240m }
    ];

    private static int _nextId = 6;

    public MenuService(IOptions<MenuOptions> options)
    {
        PageSize = Math.Clamp(options.Value.DefaultPageSize, 1, 100);
    }

    private int PageSize { get; }

    public List<MenuItem> GetAll(string? category)
    {
        var items = _menuItems
            .Where(item => string.IsNullOrWhiteSpace(category) ||
                           item.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .OrderBy(item => item.Id)
            .Take(PageSize)
            .ToList();

        return items;
    }

    public MenuItem? GetById(int id)
    {
        return _menuItems.FirstOrDefault(item => item.Id == id);
    }

    public MenuItem Create(MenuItem menuItem)
    {
        menuItem.Id = _nextId++;
        _menuItems.Add(menuItem);
        return menuItem;
    }

}
