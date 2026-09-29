using DeepDive.Models;

namespace DeepDive.ViewModels
{
    public class ForecastViewModel
    {
        public City City { get; set; }
        public WeatherForecast WeatherForecast { get; set; }
        public MarineForecast MarineForecast { get; set; }
    }
}
