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
        [JsonConverter(typeof(DoubleListConverter))]
        public List<double> SeaSurfaceTempearture { get; set; } = new List<double>();
        [JsonPropertyName("wave_height")]
        [JsonConverter(typeof(DoubleListConverter))]
        public List<double> WaveHeight { get; set; } = new List<double>();
    }

    /// <summary>
    /// Custom converter to handle null values in double lists by converting them to 0.0
    /// </summary>
    public class DoubleListConverter : JsonConverter<List<double>>
    {
        public override List<double> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = new List<double>();

            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException("Expected start of array");
            }

            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                if (reader.TokenType == JsonTokenType.Null)
                {
                    list.Add(0.0); // Convert null to 0.0
                }
                else if (reader.TokenType == JsonTokenType.Number)
                {
                    list.Add(reader.GetDouble());
                }
                else
                {
                    throw new JsonException($"Unexpected token type: {reader.TokenType}");
                }
            }

            return list;
        }

        public override void Write(Utf8JsonWriter writer, List<double> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var item in value)
            {
                writer.WriteNumberValue(item);
            }
            writer.WriteEndArray();
        }
    }
}