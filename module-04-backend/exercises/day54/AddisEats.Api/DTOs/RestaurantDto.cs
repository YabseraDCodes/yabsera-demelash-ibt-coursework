using AddisEats.Api.Models;

namespace AddisEats.Api.DTOs;

public record RestaurantDto(
    int Id,
    string Name,
    string Area,
    decimal DeliveryFeeEtb,
    IReadOnlyList<MenuItem> Menu);
