namespace AddisEats.Catalog;

//public record Customer(string Name, string PhoneNumber);

public class Customer
{
    public string Name { get; set; }

    public string PhoneNumber { get; set; }

    public Customer(string name, string phoneNumber)
    {
        Name = name;
        PhoneNumber = phoneNumber;
    }
}

//Records compare values while classes compare references.