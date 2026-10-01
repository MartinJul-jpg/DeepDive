namespace DeepDive.Models
{
    public class GeocodingResponse
    {
        public List<City> Results { get; set; }
    }

    public class City
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Country { get; set; }
    }
}
