using AddisEats.Api.Models;

namespace AddisEats.Api.Services.MenuServices;

public interface IMenuService
{
    List<MenuItem> GetAll(string? category);
    MenuItem? GetById(int id);
    MenuItem Create(MenuItem menuItem);
}
