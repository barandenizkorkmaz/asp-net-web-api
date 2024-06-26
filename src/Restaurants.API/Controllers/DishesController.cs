﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDishesByRestaurantId;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurantId;
using Restaurants.Application.Dishes.Queries.GetDishesByRestaurantId;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers;

[Route("api/restaurant/{restaurantId}/dishes")]
[ApiController]
[Authorize]
public class DishesController(IMediator mediator) : ControllerBase
{
    // observe that restaurantId is a route paramater in controller-level
    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;
        var dishId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetDishByIdForRestaurant), new {restaurantId, dishId }, null); // last parameter is the object that we are returning to the client
    }

    [HttpGet]
    [Authorize(Policy = PolicyNames.AtLeast18)]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetAllByRestaurantId([FromRoute] int restaurantId)
    {
        var dishes = await mediator.Send(new GetDishesByRestaurantIdQuery(restaurantId));
        return Ok(dishes);
    }

    [HttpGet("{dishId}")]
    public async Task<ActionResult<DishDto>> GetDishByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        var dishes = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
        return Ok(dishes);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteDishesByRestaurantId([FromRoute] int restaurantId)
    {
        await mediator.Send(new DeleteDishesByRestaurantIdCommand(restaurantId));
        return NoContent();
    }
}
