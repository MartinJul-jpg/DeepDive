using System.Text.Json.Serialization;

namespace DeepDive.Models
{
    public class MarineRespone()
    {
        [JsonPropertyName("hourly")]
        public MarineForecast HourlyRespone { get; set; }
    }

    public class MarineForecast
    {
        [JsonPropertyName("time")]
        public List<DateTime> TimeStamps { get; set; } = new List<DateTime>();
        [JsonPropertyName("sea_surface_temperature")]
        public List<double> SeaSurfaceTempearture { get; set; } = new List<double>();
        [JsonPropertyName("wave_height")]
        public List<double> WaveHeight { get; set; } = new List<double>();
    }
}
