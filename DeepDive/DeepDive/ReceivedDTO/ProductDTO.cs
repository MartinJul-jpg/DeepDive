using DeepDive.Models;

namespace DeepDive.ReceivedDTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public double Price { get; set; }
        public string Brand { get; set; }
        public int Amount { get; set; }
        public string ImgPath { get; set; }
        public byte[] RowVersion { get; set; }

        public string Model { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public string Thickness { get; set; }
        public string FirstStage { get; set; }
        public string SecondStage { get; set; }
        public string Octopus { get; set; }
        public string Volume { get; set; }

        public string ProductType { get; set; }
    }
}
