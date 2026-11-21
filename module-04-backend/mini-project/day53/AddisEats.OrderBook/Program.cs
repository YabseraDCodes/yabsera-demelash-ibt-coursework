using AddisEats.OrderBook;

var menu = new MenuService(new List<MenuItem>
{
    new(1, "Doro wot", "Main", 420m),
    new(2, "Shiro", "Main", 180m),
    new(3, "Tibs", "Main", 350m),
    new(4, "Coffee", "Drink", 45m)
});

Console.WriteLine("Addis Eats menu");
menu.Items.ToList().ForEach(item => Console.WriteLine($"{item.Id}. {item.Name} - {item.Price} ETB"));

var firstOrder = new Order(
    "Mimi",
    "Bole",
    new List<OrderLine>
    {
        new(menu.FindById(1), 1),
        new(menu.FindById(4), 2)
    });

Console.WriteLine($"\nOrder for {firstOrder.Customer} in {firstOrder.Area}");
firstOrder.Lines.ToList().ForEach(line => Console.WriteLine($"{line.Quantity} x {line.Item.Name}: {line.Item.Price * line.Quantity} ETB"));
Console.WriteLine($"Total: {firstOrder.Total} ETB\n");

var book = new AddisEats.OrderBook.OrderBook(new List<Order>
{
    firstOrder,
    new("Dawit", "Piazza", new List<OrderLine> { new(menu.FindById(2), 2), new(menu.FindById(4), 1) }),
    new("Hana", "Bole", new List<OrderLine> { new(menu.FindById(3), 1), new(menu.FindById(4), 1) })
});

Console.WriteLine("Bole orders is listed below");
book.GetOrdersForArea("Bole").ToList().ForEach(order => Console.WriteLine($"{order.Customer}: {order.Total} ETB"));
Console.WriteLine($"Revenue: {book.GetRevenue()} ETB");
Console.WriteLine($"Busiest category: {book.GetBusiestCategory() ?? "No order have been placed yet"}");
Console.WriteLine("Page 1");
book.GetPage(1).ToList().ForEach(order => Console.WriteLine($"{order.Customer} - {order.Area}: {order.Total} ETB"));
