using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQuery(int id) : IRequest<RestaurantDto?>
{
    public int Id { get; } = id; // We want to ensure that this object can be written only during initialization!

    // Another way
    // public int Id { get; init; }
}
