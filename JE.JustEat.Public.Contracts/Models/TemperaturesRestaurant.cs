using JE.JustEat.Public.Contracts.Converters;
using Newtonsoft.Json;

namespace JE.JustEat.Public.Contracts
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class TemperaturesRestaurant
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("RatingStars")]
        public double RatingStars { get; set; }

        [JsonProperty("CuisineTypes")]
        public CuisineType[] CuisineTypes { get; set; }

    }
}
