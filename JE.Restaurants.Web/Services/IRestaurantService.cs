using System.Threading.Tasks;
using JE.Restaurant.WebApi.Dtos;

namespace JE.Restaurant.WebApi.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<RestaurantDto[]> GetRestaurantsByPostCodeAsync(string postCode);
    }
}
