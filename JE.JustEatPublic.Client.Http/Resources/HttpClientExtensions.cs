using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JE.JustEat.Public.Client
{
    public static class HttpClientExtensions
    {
        public static async Task<TResponse> GetAsync<TResponse>(this HttpClient httpClient, string endpoint)
        {
            return await GetAsync<object, TResponse>(httpClient, endpoint, string.Empty);
        }

        public static async Task<TResponse> GetAsync<TRequest, TResponse>(this HttpClient httpClient, string endpoint, string query) where TRequest : class
        {
            var requestString = $"{endpoint}{query}";

            var response = await httpClient.GetAsync(requestString);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseContent, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }
    }
}
