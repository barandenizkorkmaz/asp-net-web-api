using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Requests;
using Restaurants.Application.Services;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IRestaurantService restaurantService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(CreateRestaurantDto createRestaurantDto)
    {
        int id = await restaurantService.Create(createRestaurantDto);
        return CreatedAtAction(nameof(GetRestaurantById), new { id }, null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var restaurants = await restaurantService.GetAllRestaurants();
        return Ok(restaurants);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetRestaurantById([FromRoute] int id)
    {
        var restaurant = await restaurantService.GetRestaurantById(id);
        if (restaurant == null)
        {
            return NotFound();
        }
        return Ok(restaurant);
    }
}
