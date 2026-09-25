using DeepDive.Models;

namespace DeepDive.Services
{
    public interface IWeatherHttpService
    {
        Task<City> GetCityByNameAsync(string name);

        Task<WeatherForecast> GetMeanWeatherForecastAsync(double latitude, double longitude);

        Task<MarineForecast> GetMarinaForecastAsync(double latitude, double longitude);
    }
}
