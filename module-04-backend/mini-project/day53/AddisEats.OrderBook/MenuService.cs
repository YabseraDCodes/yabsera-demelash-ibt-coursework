namespace AddisEats.OrderBook;

public sealed class MenuService
{
    public IReadOnlyList<MenuItem> Items { get; }

    public MenuService(IEnumerable<MenuItem> items)
    {
        Items = items.ToList();
    }

    public MenuItem FindById(int id) => Items.First(item => item.Id == id);
}
