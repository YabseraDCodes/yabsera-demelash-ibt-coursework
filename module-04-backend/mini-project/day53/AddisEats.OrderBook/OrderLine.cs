namespace AddisEats.OrderBook;

public sealed record OrderLine
{
    public MenuItem Item { get; }
    public int Quantity { get; }

    public OrderLine(MenuItem item, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be more than zero.", nameof(quantity));

        Item = item ?? throw new ArgumentNullException(nameof(item));
        Quantity = quantity;
    }
}
