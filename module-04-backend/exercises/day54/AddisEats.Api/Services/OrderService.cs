using AddisEats.Api.Models;

namespace AddisEats.Api.Services;

public interface IOrderService
{
    Order? GetById(int id);
    Order Create(CreateOrderRequest request);
}

public sealed class OrderService : IOrderService
{
    private static readonly List<Order> Orders = [];
    private static int _nextId = 1;

    public Order? GetById(int id) => Orders.FirstOrDefault(order => order.Id == id);

    public Order Create(CreateOrderRequest request)
    {
        var order = new Order
        {
            Id = _nextId++,
            RestaurantId = request.RestaurantId,
            MenuItemIds = request.MenuItemIds,
            Notes = request.Notes
        };
        Orders.Add(order);
        return order;
    }
}
