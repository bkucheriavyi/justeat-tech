using System.Net.Http;
using System.Threading.Tasks;
using JE.JustEat.Public.Client.Http.Configuration;
using JE.JustEat.Public.Contracts;

namespace JE.JustEat.Public.Client
{
    public class RestaurantResource : IRestaurantResource
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JustEatPublicApiClientConfig _config;

        public RestaurantResource(IHttpClientFactory httpClientFactory, JustEatPublicApiClientConfig config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public Task<Temperatures> GetRestaurantByPostCodeAsync(string postCode)
        {
            var client = _httpClientFactory.CreateClient(_config.ServiceUrl);
            return client.GetAsync<Temperatures>($"restaurants/bypostcode/{postCode}");
        }
    }
}
