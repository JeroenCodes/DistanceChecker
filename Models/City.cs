using System.Text.Json.Serialization;

namespace DistanceChecker.Models
{
    public class City
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("latitude")]
        public double Latitude {get;set;}
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("population")]
        public int Population { get; set; }
        [JsonPropertyName("is_capital")]
        public bool Is_Capital { get; set; }
    }
}
