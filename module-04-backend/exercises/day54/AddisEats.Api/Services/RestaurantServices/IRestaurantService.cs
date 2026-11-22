using AddisEats.Api.DTOs;

namespace AddisEats.Api.Services.RestaurantServices;

public interface IRestaurantService
{
    IReadOnlyList<RestaurantDto> GetAll(RestaurantQuery query);
    RestaurantDto? GetById(int id);
    RestaurantDto? GetByName(string name);
}
