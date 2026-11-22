using AddisEats.Api.DTOs;
using AddisEats.Api.Mappers;
using AddisEats.Api.Models;

namespace AddisEats.Api.Services.RestaurantServices;

public class RestaurantService : IRestaurantService
{
    private static readonly List<Restaurant> Restaurants = Seed();

    public IReadOnlyList<RestaurantDTO> GetAll(RestaurantQuery query) => Restaurants
        .Where(r => string.IsNullOrWhiteSpace(query.Area) || r.Area.Equals(query.Area, StringComparison.OrdinalIgnoreCase))
        .Where(r => query.Areas is null || query.Areas.Length == 0 || query.Areas.Contains(r.Area, StringComparer.OrdinalIgnoreCase))
        .Where(r => query.MaxFeeEtb is null || r.DeliveryFeeEtb <= query.MaxFeeEtb.Value)
        .OrderBy(r => r.Id)
        .Skip(((query.Page ?? 1) - 1) * (query.Limit ?? 10))
        .Take(query.Limit ?? 10)
        .Select(r => r.ToDTO())
        .ToList();

    public RestaurantDTO? GetById(int id) => Restaurants
        .Where(r => r.Id == id)
        .Select(r => r.ToDTO())
        .FirstOrDefault();

    public IReadOnlyList<MenuItem>? GetMenu(int id) => Restaurants
        .Where(r => r.Id == id)
        .Select(r => (IReadOnlyList<MenuItem>)r.Menu.ToList())
        .FirstOrDefault();

    private static List<Restaurant> Seed()
    {
        string[] areas = ["Bole", "Piassa", "Kazanchis", "Arat Kilo"];
        string[] names = ["Habesha Table", "Addis Garden", "Blue Nile Kitchen", "Lucy Cafe", "Merkato Grill", "Yod Abyssinia", "Selam Bistro", "Tana Restaurant", "Enat Buna", "Abay Lounge", "Kaffa House", "Mesob Kitchen"];
        string[] cuisines = ["Ethiopian", "Ethiopian", "Fusion", "Ethiopian", "Grill", "Ethiopian", "Cafe", "Ethiopian", "Cafe", "Fusion", "Cafe", "Ethiopian"];
        string[] dishes = ["Doro Wot", "Tibs", "Shiro", "Kitfo", "Sambusa", "Injera Firfir"];
        return Enumerable.Range(0, 12).Select(i => new Restaurant
        {
            Id = i + 1,
            Name = names[i],
            Area = areas[i % areas.Length],
            Cuisine = cuisines[i],
            DeliveryFeeEtb = 30m + (i % 4) * 15m,
            Menu = dishes.Select((dish, j) => new MenuItem
            {
                Id = i * dishes.Length + j + 1,
                Name = dish,
                Category = j == 4 ? "Starter" : "Main",
                Price = 100m + (j * 75m) + (i * 5m)
            }).ToList()
        }).ToList();
    }
}
