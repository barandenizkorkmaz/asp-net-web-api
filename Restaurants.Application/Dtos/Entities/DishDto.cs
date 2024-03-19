using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dtos.Entities;

public class DishDto
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public decimal Price { get; set; }

    public int? KiloCalories { get; set; }

    /*
     * Mapping logic has been done by AutoMapper.
     * 
    public static DishDto FromEntity(Dish dish)
    {
        return new DishDto()
        {
            Id = dish.Id,
            Name = dish.Name,
            Description = dish.Description,
            Price = dish.Price,
            KiloCalories = dish.KiloCalories
        };
    }
    */
}
