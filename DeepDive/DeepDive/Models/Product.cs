using System.ComponentModel.DataAnnotations;

namespace DeepDive.Models
{
    public abstract class Product
    {
        public int ProductId { get; set; }
        public double Price { get; set; }
        public string Brand { get; set; }
        [Range(0, int.MaxValue)]
        public int Amount { get; set; } = 0;
        public abstract string GetUniqueInfo();
        public abstract string GetUniqueInfoPage();

        public string GetProductInfo() 
        {
            return $"Price: {Price}{"\n"} Brand: {Brand}{"\n"}";
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
