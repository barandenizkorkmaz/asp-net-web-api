using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dtos.Entities;

public class DishesProfile : Profile
{
    public DishesProfile()
    {
        CreateMap<Dish, DishDto>();
    }
}
