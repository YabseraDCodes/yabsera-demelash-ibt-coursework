# Addis Eats Order Book

Addis Eats Order Book is a small C# console app for a food menu and customer orders. It starts with a few menu items, makes some sample orders, and prints order details and reports in the console.

## What it does

- Keeps menu items with an ID, name, category, and price in ETB.
- Makes order lines by pairing a menu item with a quantity.
- Stores each order with a customer, area, order lines, and a total worked out from the lines.
- Shows orders for a chosen area, with the largest total first.
- Adds up the revenue from all orders.
- Finds the category with the most items ordered. If there are no orders, it reports that there is no busiest category yet.
- Shows orders in pages of five, so page 2 starts after the five orders on page 1.

Prices and quantities must be more than zero. Prices and totals use `decimal` values so money is not stored as a floating-point number.

## Project files

- `MenuItem.cs` holds the menu item record.
- `OrderLine.cs` holds the order line record.
- `Order.cs` holds one customer order and works out its total.
- `MenuService.cs` keeps the menu items and looks them up by ID.
- `OrderBook.cs` keeps the orders and makes the reports.
- `Program.cs` runs the sample menu and orders.

## Run it

You need the .NET 10 SDK. Open a terminal in this folder and run:

```bash
dotnet run
```

The program prints the menu, one order with its lines and total, then area orders, revenue, the busiest category, and the first page of orders. The sample data is written in `Program.cs`; edit it to try different menu items or orders.
