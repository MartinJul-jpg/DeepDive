using DeepDive.Models;

namespace DeepDive.Services
{
    public class WeatherHttpService : IWeatherHttpService
    {
        public Task<City> GetCityByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<MarineForecast> GetMarinaForecast(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public Task<WeatherForecast> GetWeatherForecast(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }
    }
}
