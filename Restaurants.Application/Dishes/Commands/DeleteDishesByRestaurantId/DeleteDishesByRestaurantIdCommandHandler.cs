using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishesByRestaurantId;

public class DeleteDishesByRestaurantIdCommandHandler(ILogger<DeleteDishesByRestaurantIdCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository) : IRequestHandler<DeleteDishesByRestaurantIdCommand>
{
    public async Task Handle(DeleteDishesByRestaurantIdCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning("Removing all dishes from restaurant with id {RestaurantId}", request.RestaurantId);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        await dishesRepository.Delete(restaurant.Dishes);
    }
}
