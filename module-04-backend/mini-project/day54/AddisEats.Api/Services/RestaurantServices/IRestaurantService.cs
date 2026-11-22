using AddisEats.Api.DTOs;
using AddisEats.Api.Models;

namespace AddisEats.Api.Services.RestaurantServices;

public interface IRestaurantService
{
    IReadOnlyList<RestaurantDTO> GetAll(string? area, int page, int pageSize);
    RestaurantDTO? GetById(int id);
    IReadOnlyList<MenuItem>? GetMenu(int id);
}
