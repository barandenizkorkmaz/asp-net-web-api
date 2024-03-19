using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Services;

internal class RestaurantService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantService> logger, IMapper mapper) : IRestaurantService
{
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants...");
        var restaurants = await restaurantsRepository.GetAllAsync();
        /*
         * Manual mapping between Restaurant entity and RestaurantDto
            var restaurantsDtos = restaurants.Select(RestaurantDto.FromEntity);
        */
        // Better way of mapping
        var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        return restaurantsDtos!;
    }

    public async Task<RestaurantDto?> GetRestaurantById(int id)
    {
        logger.LogInformation($"Getting restaurant with id {id}");
        var restaurant = await restaurantsRepository.GetByIdAsync(id);
        /* Manual mapping between Restaurant entity and RestaurantDto
            var restaurantDto = RestaurantDto.FromEntity(restaurant);
        */
        // Better way of mapping
        var restaurantDto = mapper.Map<RestaurantDto?>(restaurant);
        return restaurantDto;
    }
}
