using AddisEats.Catalog;

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


