using AddisEats.Api.DTOs;
using AddisEats.Api.Models;

namespace AddisEats.Api.Mappers;

public static class RestaurantMapper
{
    public static RestaurantDTO ToDTO(this Restaurant restaurant) => new(
        restaurant.Id,
        restaurant.Name,
        restaurant.Area,
        restaurant.Cuisine,
        restaurant.DeliveryFeeEtb,
        restaurant.Menu.ToList());
}
