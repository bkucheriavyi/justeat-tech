using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JE.JustEat.Public.Client;
using JE.JustEat.Public.Contracts;
using JE.Restaurant.WebApi.Dtos;
using JE.Restaurant.WebApi.Services.Interfaces;

namespace JE.Restaurant.WebApi.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IJustEatPublicApiClient _justEatClient;
        private readonly IMapper _mapper;

        public RestaurantService(IJustEatPublicApiClient justEatClient, IMapper mapper)
        {
            _justEatClient = justEatClient;
            _mapper = mapper;
        }

        public async Task<RestaurantDto[]> GetRestaurantsByPostCodeAsync(string postCode)
        {
            var response = await _justEatClient.RestaurantResource.GetRestaurantByPostCodeAsync(postCode);
            var result = _mapper.Map<TemperaturesRestaurant[], RestaurantDto[]>(response.Restaurants);

            return result;
        }
    }
}
