Console.WriteLine("--------------------------------");
Console.WriteLine("  WELCOME TO TOPUP CALCULATOR");
Console.WriteLine("--------------------------------");

Console.Write("Enter your name: ");
string name = Console.ReadLine() ?? "";
Console.WriteLine("Select your mobile network 1. EthioTelecom,   2. Safaricom: ");
string telecom = Console.ReadLine() ?? "";
Console.Write("Enter amount you would like to top-up: ");
string inputAmount = Console.ReadLine() ?? "";

decimal amount;

if (telecom != "1" && telecom != "2")
{
    Console.WriteLine("Please choose either 1 for EthioTelecom or 2 for Safaricom");
}

if (decimal.TryParse(inputAmount, out amount))
{
    if (amount <= 0)
    {
        Console.WriteLine("Please enter a valid amount.");
    }
    else if (amount > 5000)
    {
        Console.WriteLine("Please enter an amount less than 5000.");
    }
    else
    {
        Console.WriteLine("Converting success" + $"{amount:N2}");
    }
}
else
{
    Console.WriteLine("Please enter a number as amount.");
}

decimal bonusRate = telecom switch
{
    "1" => 0.05m,
    "2" => 0.03m,
    _ => 0m
};

decimal bonus = amount * bonusRate;

if (amount >= 1000)
{
    bonus += 20m;
}

decimal finalCredit = amount + bonus;

//getting network name
string network = telecom switch
{
    "1" => "Ethio Telecom",
    "2" => "Safaricom",
    _ => "Unknown"
};

Console.WriteLine();
Console.WriteLine("--------------------------------");
Console.WriteLine("          TOP-UP RECEIPT");
Console.WriteLine("--------------------------------");

Console.WriteLine($"Customer: {name}");
Console.WriteLine($"Network: {network}");
Console.WriteLine($"Amount: {amount:N2} ETB");
Console.WriteLine($"Bonus: {bonus:N2} ETB");
Console.WriteLine($"Final Credit: {finalCredit:N2} ETB");

Console.WriteLine("--------------------------------");