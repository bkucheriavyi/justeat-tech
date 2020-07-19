using JE.JustEat.Public.Contracts.Converters;
using Newtonsoft.Json;

namespace JE.JustEat.Public.Contracts
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class Temperatures
    {
        [JsonProperty("Restaurants")]
        public TemperaturesRestaurant[] Restaurants { get; set; }
    }
}
