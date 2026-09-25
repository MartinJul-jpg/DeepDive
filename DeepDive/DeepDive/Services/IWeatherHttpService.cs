using DeepDive.Models;

namespace DeepDive.Services
{
    public interface IWeatherHttpService
    {
        Task<City> GetCityByName(string name);

        Task<WeatherForecast> GetWeatherForecast(double latitude, double longitude);

        Task<MarineForecast> GetMarinaForecast(double latitude, double longitude);
    }
}
