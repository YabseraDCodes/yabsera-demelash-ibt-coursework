namespace AddisEats.Catalog;

public class FiftyBirrOffPolicy : IDiscountPolicy
{
    public decimal Apply(decimal amount)
    {
        return amount - 50m;
    }
}