using System.Text.Json;
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

        public bool HasMarineWeatherStation { get; set; }

        public MarineForecast GetMeanMarineForecat()
        {
            List<Double> meanSeaSurfaceTemperature = new List<Double>();
            List<Double> meanWaveHeight = new List<Double>();

            List<Double> tempList = new List<Double>();
            int count = 0;


            foreach (double temp in SeaSurfaceTempearture)
            {
                if (count < 23)
                {
                    tempList.Add(temp);
                    count++;
                }
                else
                {
                    double meanTempForDay = tempList.Sum() / 24;
                    meanSeaSurfaceTemperature.Add(meanTempForDay);
                    tempList.Clear();
                    count = 0;

                    // Add the data missed by the if else
                    tempList.Add(temp);
                    count++;
                }
            }

            tempList.Clear();
            count = 0;

            foreach (double wave in WaveHeight)
            {
                if (count < 23)
                {
                    tempList.Add(wave);
                    count++;
                }
                else
                {
                    double meanWaveForDay = tempList.Sum() / 24;
                    meanWaveHeight.Add(meanWaveForDay);
                    tempList.Clear();
                    count = 0;

                    // Add the data missed by the if else
                    tempList.Add(wave);
                    count++;
                }
            }

            return new MarineForecast
            {
                TimeStamps = TimeStamps,
                SeaSurfaceTempearture = meanSeaSurfaceTemperature,
                WaveHeight = meanWaveHeight,
                HasMarineWeatherStation = this.HasMarineWeatherStation
            };
        }
    }
}