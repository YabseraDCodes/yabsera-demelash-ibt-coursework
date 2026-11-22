using AddisEats.Api.DTOs;
using AddisEats.Api.Models;

namespace AddisEats.Api.Services;

public interface IRestaurantService
{
    IReadOnlyList<RestaurantDto> Search(string? area, int page, int limit);
    RestaurantDto? GetById(int id);
    IReadOnlyList<MenuItem>? GetMenu(int id);
}
