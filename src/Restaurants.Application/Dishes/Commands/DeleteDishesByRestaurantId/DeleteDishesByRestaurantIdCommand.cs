using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDishesByRestaurantId;

public class DeleteDishesByRestaurantIdCommand(int restaurantId) : IRequest
{
    public int RestaurantId { get; } = restaurantId;
}
