namespace AddisEats.Catalog;

public class TenPercentOffPolicy : IDiscountPolicy
{
    public decimal Apply(decimal amount)
    {
        return amount * 0.9m;
    }
}