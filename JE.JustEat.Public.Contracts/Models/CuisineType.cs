using JE.JustEat.Public.Contracts.Converters;
using Newtonsoft.Json;

namespace JE.JustEat.Public.Contracts
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class CuisineType
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}