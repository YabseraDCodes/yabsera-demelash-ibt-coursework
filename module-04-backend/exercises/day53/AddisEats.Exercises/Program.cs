using AddisEats.Catalog;

Restaurant restaurant = new()
{
    Id = 1,
    Name = "Tomoca",
    Area = "Piassa"
};

restaurant.Name = "Tomoca Cafe";

Console.WriteLine(restaurant.Id);
Console.WriteLine(restaurant.DisplayName);


// EXERCISE 1
//Console.WriteLine(DeliveryFeeHelper.DeliveryFee("Bole"));
//Console.WriteLine(DeliveryFeeHelper.DeliveryFee(area: "Kazanchis"));
//Console.WriteLine(DeliveryFeeHelper.DeliveryFee("Piassa", true));

// EXERCISE 2
//Describer.Describe(10);
//Describer.Describe("Addis Eats");
//Describer.Describe(99.5m);