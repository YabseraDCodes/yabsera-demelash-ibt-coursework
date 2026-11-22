using AddisEats.Api.Models;

namespace AddisEats.Api.Services;

public static class RestaurantData
{
    public static readonly List<Restaurant> Restaurants =
    [
        Make(1, "Kategna", "Bole", 35m, 0.12m, ["Doro Wot", "Tibs", "Shiro"]),
        Make(2, "Yod Abyssinia", "Bole", 45m, 0.14m, ["Kitfo", "Gored Gored"]),
        Make(3, "2000 Habesha", "Bole", 30m, 0.11m, ["Beyaynetu", "Firfir"]),
        Make(4, "Sishu", "Bole", 40m, 0.13m, ["Burger", "Fries"]),
        Make(5, "Lucy Gazebo", "Piassa", 25m, 0.10m, ["Injera Firfir", "Shiro"]),
        Make(6, "Addis Ababa Restaurant", "Piassa", 35m, 0.12m, ["Doro Wot"]),
        Make(7, "Five Loaves", "Piassa", 30m, 0.11m, ["Pasta", "Sandwich"]),
        Make(8, "Castelli", "Kazanchis", 50m, 0.15m, ["Lasagna", "Pizza"]),
        Make(9, "Effoi Pizza", "Kazanchis", 40m, 0.13m, ["Margherita Pizza", "Pasta"]),
        Make(10, "Savor", "Kazanchis", 35m, 0.12m, ["Tibs", "Salad"]),
        Make(11, "Enrico Pastry", "Arat Kilo", 25m, 0.10m, ["Croissant", "Cake"]),
        Make(12, "Kaffa Coffee", "Arat Kilo", 20m, 0.09m, [])
    ];

    private static Restaurant Make(
        int id, string name, string area, decimal fee, decimal commission, string[] dishNames)
    {
        var menu = dishNames.Select((dish, index) =>
            new MenuItem(index + 1, dish, 100m + (index * 75m))).ToList();

        return new Restaurant
        {
            Id = id,
            Name = name,
            Area = area,
            DeliveryFee = fee,
            OwnerPhone = $"+251 91 000 {id:0000}",
            CommissionRate = commission,
            Menu = menu
        };
    }
}
