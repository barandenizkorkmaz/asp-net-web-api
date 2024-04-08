using MediatR;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommand(int id) : IRequest // If we don't return any value, we don't need generic parameter.
{
    public int Id { get; } = id;
}
