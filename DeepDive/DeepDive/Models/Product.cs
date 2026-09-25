using System.ComponentModel.DataAnnotations;

namespace DeepDive.Models
{
    public abstract class Product
    {
        public int ProductId { get; set; }
        public double Price { get; set; }
        public string Brand { get; set; } = string.Empty;
        [Range(0, int.MaxValue)]
        public int Amount { get; set; } = 0;
        public string ImgPath { get; set; } = "~/lib/custom/resource/product-placeholder.png";
        public List<Package_Product> Package_Products { get; set; } = new List<Package_Product>();
        public List<Product_Booking> Product_Bookings { get; set; } = new List<Product_Booking>();

        public abstract string GetUniqueInfo();
        public abstract string GetUniqueInfoPage();

        public string GetProductInfo() 
        {
            return $"Price: {Price}{"\n"} Brand: {Brand}{"\n"}";
        }

        public abstract string GetUniqueSearchBlob();

        public string GetSearchBlob()
        {
            return $"{Brand} {GetUniqueSearchBlob()}";
        }

        public string GetProductBrand()
        {
            return $"{Brand}";
        }

        public string GetProductPrice()
        {
            return $"{Price}";
        }

        public string GetAmount()
        {
            return $"{Amount}";
        }
    }
}
