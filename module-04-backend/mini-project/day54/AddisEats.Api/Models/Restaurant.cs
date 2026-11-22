namespace AddisEats.Api.Models;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Cuisine { get; set; } = string.Empty;
    public decimal DeliveryFeeEtb { get; set; }
    public List<MenuItem> Menu { get; set; } = [];
}
