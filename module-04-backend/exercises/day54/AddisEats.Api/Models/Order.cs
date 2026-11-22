namespace AddisEats.Api.Models;

public enum OrderStatus
{
    Pending,
    Confirmed,
    Preparing,
    Delivered,
    Cancelled
}

public class Order
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public List<int> MenuItemIds { get; set; } = [];
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string? Notes { get; set; }
}

public record CreateOrderRequest(int RestaurantId, List<int> MenuItemIds, string? Notes = null);
