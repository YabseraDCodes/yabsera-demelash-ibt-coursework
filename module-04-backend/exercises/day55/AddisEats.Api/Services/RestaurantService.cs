using AddisEats.Api.DTOs;
using AddisEats.Api.Models;

namespace AddisEats.Api.Services;

public class RestaurantService : IRestaurantService
{
    public IReadOnlyList<RestaurantDto> Search(string? area, int page, int limit)
    {
        return RestaurantData.Restaurants
            .Where(restaurant => string.IsNullOrWhiteSpace(area) ||
                                 restaurant.Area.Equals(area, StringComparison.OrdinalIgnoreCase))
            .OrderBy(restaurant => restaurant.Id)
            .Skip((page - 1) * limit)
            .Take(limit)
            .Select(restaurant => restaurant.ToDto())
            .ToList();
    }

    public RestaurantDto? GetById(int id)
    {
        var restaurant = RestaurantData.Restaurants.FirstOrDefault(item => item.Id == id);
        return restaurant?.ToDto();
    }

    public IReadOnlyList<MenuItem>? GetMenu(int id)
    {
        var restaurant = RestaurantData.Restaurants.FirstOrDefault(item => item.Id == id);
        return restaurant?.Menu.ToList();
    }
}
