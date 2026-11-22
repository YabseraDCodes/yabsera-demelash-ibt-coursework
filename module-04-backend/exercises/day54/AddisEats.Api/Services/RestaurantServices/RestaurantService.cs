using AddisEats.Api.DTOs;
using AddisEats.Api.Models;

namespace AddisEats.Api.Services.RestaurantServices;

public class RestaurantService : IRestaurantService
{
    private static readonly List<Restaurant> Restaurants = Seed();

    public IReadOnlyList<RestaurantDto> GetAll(RestaurantQuery query) => Restaurants
        .Where(restaurant => string.IsNullOrWhiteSpace(query.Name) || restaurant.Name.Contains(query.Name, StringComparison.OrdinalIgnoreCase))
        .Where(restaurant => string.IsNullOrWhiteSpace(query.Area) ||
                             restaurant.Area.Equals(query.Area, StringComparison.OrdinalIgnoreCase))
        .Where(restaurant => query.Areas is null || query.Areas.Length == 0 ||
                             query.Areas.Contains(restaurant.Area, StringComparer.OrdinalIgnoreCase))
        .Where(restaurant => query.MaxFeeEtb is null || restaurant.DeliveryFeeEtb <= query.MaxFeeEtb.Value)
        .OrderBy(restaurant => restaurant.Id)
        .Skip(((query.Page ?? 1) - 1) * (query.Limit ?? 10))
        .Take(query.Limit ?? 10)
        .Select(ToDto)
        .ToList();

    public RestaurantDto? GetById(int id) => Restaurants
        .Where(restaurant => restaurant.Id == id)
        .Select(ToDto)
        .FirstOrDefault();

    private static RestaurantDto ToDto(Restaurant restaurant) => new(
        restaurant.Id,
        restaurant.Name,
        restaurant.Area,
        restaurant.DeliveryFeeEtb,
        restaurant.Menu.ToList());

    private static List<Restaurant> Seed()
    {
        string[] areas = ["Bole", "Piassa", "Kazanchis", "Arat Kilo"];
        string[] names = ["Habesha Table", "Addis Garden", "Blue Nile Kitchen", "Lucy Cafe", "Merkato Grill", "Yod Abyssinia", "Selam Bistro", "Tana Restaurant"];
        string[] dishes = ["Doro Wot", "Tibs", "Shiro"];

        return Enumerable.Range(0, names.Length).Select(index => new Restaurant
        {
            Id = index + 1,
            Name = names[index],
            Area = areas[index % areas.Length],
            DeliveryFeeEtb = 30m + (index % 4) * 15m,
            Menu = dishes.Select((dish, dishIndex) => new MenuItem
            {
                Id = index * dishes.Length + dishIndex + 1,
                Name = dish,
                Category = "Main",
                Price = 150m + dishIndex * 75m
            }).ToList()
        }).ToList();
    }
}
