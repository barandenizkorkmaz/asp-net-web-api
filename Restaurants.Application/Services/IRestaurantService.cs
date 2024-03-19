using Restaurants.Application.Dtos.Entities;
using Restaurants.Application.Dtos.Requests;

namespace Restaurants.Application.Services;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
    Task<RestaurantDto?> GetRestaurantById(int id);
    Task<int> Create(CreateRestaurantDto createRestaurantDto);
}