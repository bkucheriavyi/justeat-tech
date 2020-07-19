using System.Threading.Tasks;
using JE.JustEat.Public.Contracts;

namespace JE.JustEat.Public.Client
{
    public interface IRestaurantResource
    {
        Task<Temperatures> GetRestaurantByPostCodeAsync(string postCode);
    }
}