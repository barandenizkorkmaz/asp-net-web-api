using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Requests;

namespace Restaurants.Application.Services;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
    Task<RestaurantDto?> GetRestaurantById(int id);
    Task<int> Create(CreateRestaurantDto createRestaurantDto);
}