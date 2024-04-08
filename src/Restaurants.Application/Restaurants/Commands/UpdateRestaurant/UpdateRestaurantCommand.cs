using MediatR;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommand : IRequest
{
    public int Id { get; set; }
    // Controller will automatically check if these fields are null or not!
    public string Name { get; set; } = default!; // Make this field non-nullable.
    public string Description { get; set; } = default!; // Make this field non-nullable.
    public bool HasDelivery { get; set; }
}
