namespace AddisEats.Api.Models;

public enum OrderStatus
{
    Pending,
    Confirmed,
    Preparing,
    Ready,
    Delivered,
    Cancelled
}

public class Order
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public List<int> MenuItemIds { get; set; } = [];
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}

public record CreateOrderRequest(int RestaurantId, List<int> MenuItemIds);
public record UpdateOrderStatusRequest(OrderStatus Status);
