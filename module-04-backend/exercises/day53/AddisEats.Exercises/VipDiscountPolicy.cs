namespace AddisEats.Catalog;

public class VipDiscountPolicy : IDiscountPolicy
{
    public decimal Apply(decimal amount)
    {
        return amount * 0.80m;
    }
}