using Restaurants.Domain.Entities;

namespace Restaurants.Application.Services;

public interface IRestaurantService
{
    Task<IEnumerable<Restaurant>> GetAllRestaurants();
}