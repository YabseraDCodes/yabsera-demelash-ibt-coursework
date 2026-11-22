using AddisEats.Api.DTOs;
using AddisEats.Api.Models;

namespace AddisEats.Api.Services.RestaurantServices;

public interface IRestaurantService
{
    IReadOnlyList<RestaurantDTO> GetAll(RestaurantQuery query);
    RestaurantDTO? GetById(int id);
    IReadOnlyList<MenuItem>? GetMenu(int id);
}
