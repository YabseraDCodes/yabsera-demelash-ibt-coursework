public static class DeliveryFeeHelper
{
    public static decimal DeliveryFee(string area, bool isVip = false)
    {
        if (isVip)
            return 0m;

        return area switch
        {
            "Bole" => 80m,
            "Kazanchis" => 60m,
            "Piassa" => 50m,
            _ => 100m
        };
    }
}