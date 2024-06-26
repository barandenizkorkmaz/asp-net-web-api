﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantsRepository,
    IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        /*
         * Use serilog for logging the request object. Here `Restaurant` is a template variable.
         * Adding @ in front of the variable tells Serilog that we would like to serialize a C# object.
         */
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("{UserEmail} [{UserId}] is creating a new restaurant {@Restaurant}", 
            currentUser.Email,
            currentUser.Id,
            request);
        var restaurant = mapper.Map<Restaurant>(request);
        restaurant.OwnerId = currentUser.Id;
        logger.LogInformation("Creating a new restaurant after mapper {@Restaurant}", request);
        int id = await restaurantsRepository.Create(restaurant);
        return id;
    }
}
