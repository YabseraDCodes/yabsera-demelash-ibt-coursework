using AddisEats.Api.Models;

namespace AddisEats.Api.DTOs;

public record RestaurantDto(
    int Id,
    string Name,
    string Area,
    decimal DeliveryFee,
    IReadOnlyList<MenuItem> Menu);

public static class RestaurantDtoMap
{
    public static RestaurantDto ToDto(this Restaurant restaurant) => new(
        restaurant.Id,
        restaurant.Name,
        restaurant.Area,
        restaurant.DeliveryFee,
        restaurant.Menu.ToList());
}
