using System.ComponentModel.DataAnnotations;

namespace DiveDeepAPI.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public double Price { get; set; }
        // can also just set it to required but only do so if it is actually required

        public string Brand { get; set; } = string.Empty;
        public int Amount { get; set; } 
        public string ImgPath { get; set; } = string.Empty;
        public string UniqueInfo { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty;
        public List<int> PackageIds { get; set; } = new List<int>();
        public List<int> BookingIds { get; set; } = new List<int>();
    }
}
