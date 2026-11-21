namespace AddisEats.OrderBook;

public sealed class Order
{
    public string Customer { get; }
    public string Area { get; }
    public IReadOnlyList<OrderLine> Lines { get; }
    public decimal Total => Lines.Sum(line => line.Item.Price * line.Quantity);

    public Order(string customer, string area, IEnumerable<OrderLine> lines)
    {
        Customer = customer;
        Area = area;
        Lines = lines.ToList();
    }
}
