using System.Text.Json.Serialization;

namespace DeepDive.Models
{
    public class WeatherResponse
    {
        [JsonPropertyName("daily")]
        public WeatherForecast Daily { get; set; }
        [JsonPropertyName("hourly")]
        public WeatherForecast Hourly { get; set; }
    }

    public class WeatherForecast
    {
        [JsonPropertyName("time")]
        public List<DateTime> TimeStamps { get; set; } = new();

        [JsonPropertyName("precipitation_sum")]
        public List<double> PrecipitationSum { get; set; } = new();

        [JsonPropertyName("wind_speed_10m")]
        public List<double> WindSpeed { get; set; } = new();

        [JsonPropertyName("weather_code")]
        public List<int> WeatherCode { get; set; } = new();

        public WeatherForecast GetMeanWeatherForecast()
        {
            List<Double> meanWindSpeed = new List<Double>();

            List<Double> tempList = new List<Double>();
            int count = 0;

            foreach (double wind in WindSpeed)
            {
                if (count < 23)
                {
                    tempList.Add(wind);
                    count++;
                }
                else
                {
                    double meanWindForDay = tempList.Sum() / 24;
                    meanWindSpeed.Add(meanWindForDay);
                    tempList.Clear();
                    count = 0;

                    // Add the data missed by the if else
                    tempList.Add(wind);
                    count++;
                }
            }

            return new WeatherForecast
            {
                TimeStamps = TimeStamps,
                WindSpeed = meanWindSpeed
            };
        }

    }
}
