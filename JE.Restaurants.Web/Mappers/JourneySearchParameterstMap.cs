using AutoMapper;
using JE.JustEat.Public.Contracts;
using JE.Restaurant.WebApi.Dtos;

namespace JE.Restaurants.Web.Controllers
{
    public class TemperaturesMap : Profile
    {
        public TemperaturesMap()
        {
            CreateMap<CuisineType, string>().ConvertUsing(x => x.Name);

            CreateMap<TemperaturesRestaurant, RestaurantDto>()
                .ForMember(x => x.Name, x => x.MapFrom((s, d) => s.Name))
                .ForMember(x => x.Rating, x => x.MapFrom((s, d) => s.RatingStars))
                .ForMember(x => x.FoodTypes, x=> x.MapFrom((s,d)=> s.CuisineTypes));
        }
    }
}
