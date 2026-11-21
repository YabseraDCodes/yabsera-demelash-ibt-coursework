namespace AddisEats.Catalog;

public static class DiscountService
{
    public static void PrintResult(
        decimal amount,
        IDiscountPolicy policy)
    {
        decimal discountedAmount =
            policy.Apply(amount);

        Console.WriteLine(
            $"Original: {amount:N2}");

        Console.WriteLine(
            $"After Discount: {discountedAmount:N2}");

        Console.WriteLine();
    }
}