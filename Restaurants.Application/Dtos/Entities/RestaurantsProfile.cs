using AutoMapper;
using Restaurants.Application.Dtos.Requests;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dtos.Entities;

public class RestaurantsProfile : Profile
{
    public RestaurantsProfile()
    {
        CreateMap<Restaurant, RestaurantDto>() // Create mapping from src: Restaurant to dest RestaurantDto.
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.Dishes)); // We also need to create a mapping from Dish to DishDto

        CreateMap<CreateRestaurantDto, Restaurant>() // Create mapping from src: CreateRestaurantDto to dest Restaurant.
            .ForMember(dest => dest.Address, opt => opt.MapFrom(
                src => new Address
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    Street = src.Street,
                })); 
    }
}
