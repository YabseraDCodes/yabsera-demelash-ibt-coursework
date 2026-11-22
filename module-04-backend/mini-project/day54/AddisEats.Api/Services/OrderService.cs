using AddisEats.Api.Models;

namespace AddisEats.Api.Services;

public interface IOrderService
{
    IReadOnlyList<Order> GetAll();
    Order? GetById(int id);
    Order Create(CreateOrderRequest request);
    Order? UpdateStatus(int id, OrderStatus status);
}

public class OrderService : IOrderService
{
    private static readonly List<Order> Orders = [];
    private static int _nextId = 1;

    public IReadOnlyList<Order> GetAll() => Orders.ToList();
    public Order? GetById(int id) => Orders.FirstOrDefault(order => order.Id == id);

    public Order Create(CreateOrderRequest request)
    {
        var order = new Order { Id = _nextId++, RestaurantId = request.RestaurantId, MenuItemIds = request.MenuItemIds };
        Orders.Add(order);
        return order;
    }

    public Order? UpdateStatus(int id, OrderStatus status)
    {
        var order = GetById(id);
        if (order is not null) order.Status = status;
        return order;
    }
}
