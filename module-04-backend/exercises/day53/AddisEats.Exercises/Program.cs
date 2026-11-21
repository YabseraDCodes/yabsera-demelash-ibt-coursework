using AddisEats.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

// EXERCISE 1
//Console.WriteLine(DeliveryFeeHelper.DeliveryFee("Bole"));
//Console.WriteLine(DeliveryFeeHelper.DeliveryFee(area: "Kazanchis"));
//Console.WriteLine(DeliveryFeeHelper.DeliveryFee("Piassa", true));

// EXERCISE 2
//Describer.Describe(10);
//Describer.Describe("Addis Eats");
//Describer.Describe(99.5m);


// EXERCISE 3
Restaurant restaurant = new()
{
    Id = 1,
    Name = "Tomoca",
    Area = "Piassa"
};

restaurant.Name = "Tomoca Cafe";

Console.WriteLine(restaurant.Id);
Console.WriteLine(restaurant.DisplayName);


//EXERCISE 4
Customer c1 = new("Abebe", "0911000000");
Customer c2 = new("Abebe", "0911000000");

Console.WriteLine(c1 == c2);


// EXERCISE 5
List<Order> orders =
[
    new(1,"Abebe","Bole",650m,DateTime.Now.AddDays(-1)),
    new(2,"Sara","Kazanchis",700m,DateTime.Now.AddDays(-2)),
    new(3,"John","Bole",450m,DateTime.Now.AddDays(-3)),
    new(4,"Miki","Kazanchis",1200m,DateTime.Now.AddDays(-4)),
    new(5,"Hana","Piassa",250m,DateTime.Now.AddDays(-5))
];

for (int i = 6; i <= 30; i++)
{
    orders.Add(new Order(
        i,
        $"Customer {i}",
        i % 3 == 0 ? "Bole" :
        i % 3 == 1 ? "Kazanchis" : "Piassa",
        i * 100m,
        DateTime.Now.AddDays(-i)
    ));
}

// bole over 500ETB
var boleOrders = orders
    .Where(o => o.Area == "Bole")
    .Where(o => o.Amount > 500m)
    .Take(2)
    .ToList();

// customer names sorted
var names = orders
    .Select(o => o.CustomerName)
    .Take(5)
    .OrderBy(name => name)
    .ToList();

//total revenue for Kazanchis
var totalRevenue = orders
    .Where(o => o.Area == "Kazanchis")
    .Sum(o => o.Amount);

//page 3 at 10 per page
var page3 = orders
    .OrderByDescending(o => o.CreatedAt)
    .Skip(20)
    .Take(10)
    .ToList();

//Console.WriteLine("Bole Orders:");
//foreach (var order in boleOrders)
//    Console.WriteLine($"{order.CustomerName}: {order.Amount} ETB");

//Console.WriteLine("\nCustomer Names:");
//foreach (var name in names)
//    Console.WriteLine(name);

//Console.WriteLine($"\nKazanchis Revenue: {totalRevenue} ETB");

//Console.WriteLine("\nPage 3:");
//foreach (var order in page3)
//    Console.WriteLine($"{order.Id}: {order.CustomerName}");


// EXERCISE 6
decimal amount = 1000m;

Console.WriteLine("10% Off");
DiscountService.PrintResult( amount,new TenPercentOffPolicy());

Console.WriteLine("50 ETB Off");
DiscountService.PrintResult(amount, new FiftyBirrOffPolicy());

Console.WriteLine("VIP Discount");
DiscountService.PrintResult(amount, new VipDiscountPolicy());