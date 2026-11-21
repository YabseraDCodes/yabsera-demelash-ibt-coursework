namespace AddisEats.OrderBook;

public sealed class OrderBook
{
    private readonly List<Order> _orders;

    public OrderBook(List<Order> orders)
    {
        _orders = orders;
    }

    public IEnumerable<Order> GetOrdersForArea(string area) =>
        _orders.Where(order => order.Area.Equals(area, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(order => order.Total);

    public decimal GetRevenue() => _orders.Sum(order => order.Total);

    public string? GetBusiestCategory() => _orders
        .SelectMany(order => order.Lines)
        .GroupBy(line => line.Item.Category)
        .OrderByDescending(group => group.Sum(line => line.Quantity))
        .Select(group => group.Key)
        .FirstOrDefault();

    public IEnumerable<Order> GetPage(int pageNumber)
    {
        if (pageNumber <= 0)
            throw new ArgumentException("Page number must be more than zero.", nameof(pageNumber));

        return _orders.Skip((pageNumber - 1) * 5).Take(5);
    }
}
