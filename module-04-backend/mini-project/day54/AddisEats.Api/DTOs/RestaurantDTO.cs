using AddisEats.Api.Models;

namespace AddisEats.Api.DTOs;

public record RestaurantDTO(
    int Id,
    string Name,
    string Area,
    string Cuisine,
    IReadOnlyList<MenuItem> Menu);
