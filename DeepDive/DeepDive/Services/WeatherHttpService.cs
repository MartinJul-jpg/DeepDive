using DeepDive.Models;
using System.Globalization;

namespace DeepDive.Services
{
    public class WeatherHttpService : IWeatherHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<City?> GetCityByNameAsync(string cityName)
        {
            // https://geocoding-api.open-meteo.com/v1/search?name=Odense&count=1 
            HttpClient httpClient = _httpClientFactory.CreateClient("GeolocateClient");

            try
            {
                var response = await httpClient.GetFromJsonAsync<GeocodingResponse>($"search?name={cityName}&count=1");

                return response?.Results?.FirstOrDefault();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null; // City not found
            }
        }

        public async Task<MarineForecast?> GetMarineForecastAsync(double latitude, double longitude)
        {
            // API call expample: https://marine-api.open-meteo.com/v1/marine?latitude=55.375008&longitude=9.7083435&hourly=sea_surface_temperature,wave_height&current=sea_surface_temperature&timezone=Europe%2FCopenhagen
            HttpClient httpClient = _httpClientFactory.CreateClient("MarineClient");

            // Convert comma in double to dot to avoid issues with the API call
            string url = $"marine?latitude={latitude.ToString(CultureInfo.InvariantCulture)}"
                + $"&longitude={longitude.ToString(CultureInfo.InvariantCulture)}"
                + $"&hourly=sea_surface_temperature,wave_height"
                + $"&current=sea_surface_temperature"
                + $"&timezone=Europe%2FCopenhagen";

            MarineRespone marine = null;
            try
            {
                marine = await httpClient.GetFromJsonAsync<MarineRespone>(url);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            if (marine == null)
            {
                marine = new MarineRespone()
                {
                    HourlyRespone = new MarineForecast()
                    {
                        SeaSurfaceTempearture = new List<double>(),
                        WaveHeight = new List<double>(),
                        HasMarineWeatherStation = false
                    }
                };

                // Add 168 hours of zero data (7 days * 24 hours)
                for (int i = 0; i < 168; i++)
                {
                    marine.HourlyRespone.SeaSurfaceTempearture.Add(0.0);
                    marine.HourlyRespone.WaveHeight.Add(0.0);
                }
            }
            else
            {
                marine.HourlyRespone.HasMarineWeatherStation = true;
            }

            //var respone = await httpClient.GetFromJsonAsync<MarineRespone>(url);
            return marine?.HourlyRespone;
        }

        public async Task<WeatherForecast?> GetMeanWeatherForecastAsync(double latitude, double longitude)
        {
            // API call expample: https://api.open-meteo.com/v1/forecast?latitude=55.5059&longitude=9.7305&daily=weather_code,precipitation_sum&hourly=wind_speed_10m&timezone=Europe%2FCopenhagen
            HttpClient httpClient = _httpClientFactory.CreateClient("WeatherClient");

            // Convert comma in double to dot to avoid issues with the API call
            string url = $"forecast?latitude={latitude.ToString(CultureInfo.InvariantCulture)}"
                + $"&longitude={longitude.ToString(CultureInfo.InvariantCulture)}"
                + $"&daily=precipitation_sum,weather_code"
                + $"&hourly=wind_speed_10m"
                + $"&wind_speed_unit=ms"
                + $"&timezone=Europe%2FCopenhagen";

            try
            {
                var response = await httpClient.GetFromJsonAsync<WeatherResponse>(url);

                var dailyForecast = response?.Daily;
                var hourlyForecast = response?.Hourly;

                if (dailyForecast != null && hourlyForecast != null)
                {
                    dailyForecast.WindSpeed = hourlyForecast.WindSpeed ?? new List<double>();
                }

                return dailyForecast;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null; // Latitude and longtitude wrong, forecast not found
            }
        }
    }
}
