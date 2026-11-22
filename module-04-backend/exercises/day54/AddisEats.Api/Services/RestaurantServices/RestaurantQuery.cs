namespace AddisEats.Api.Services.RestaurantServices;

public class RestaurantQuery
{
    public string? Area { get; set; }
    public decimal? MaxFeeEtb { get; set; }
    public string[]? Areas { get; set; }
    public int? Page { get; set; } = 1;
    public int? Limit { get; set; } = 10;
}
