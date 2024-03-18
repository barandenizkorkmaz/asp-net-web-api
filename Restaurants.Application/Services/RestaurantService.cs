using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Services;

internal class RestaurantService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantService> logger) : IRestaurantService
{
    public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants...");
        var restaurants = await restaurantsRepository.GetAllAsync();
        return restaurants;
    }

    public async Task<Restaurant?> GetRestaurantById(int id)
    {
        logger.LogInformation($"Getting restaurant with id {id}");
        var restaurant = await restaurantsRepository.GetByIdAsync(id);
        return restaurant;
    }
}
