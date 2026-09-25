using System.Text.Json.Serialization;

namespace DeepDive.Models
{
    public class WeatherResponse
    {
        [JsonPropertyName("daily")]
        public WeatherForecast Daily { get; set; }
    }

    public class WeatherForecast
    {
        [JsonPropertyName("time")]
        public List<DateTime> TimeStamps { get; set; } = new();

        [JsonPropertyName("precipitation_sum")]
        public List<double> PrecipitationSum { get; set; } = new();

        [JsonPropertyName("wind_speed_10m_max")]
        public List<double> WindSpeed10mMax { get; set; } = new();

        [JsonPropertyName("weather_code")]
        public List<int> WeatherCode { get; set; } = new();
    }
}
