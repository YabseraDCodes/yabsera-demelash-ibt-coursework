namespace AddisEats.Api;

public sealed class DeliveryOptions
{
    public const string SectionName = "Delivery";
    public decimal FreeAboveEtb { get; set; }
    public decimal DefaultFeeEtb { get; set; }
}
