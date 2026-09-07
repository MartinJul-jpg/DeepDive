namespace DeepDive.Models
{
    public abstract class Product
    {
        public int ProductId { get; set; }
        public double Price { get; set; }
        public string Brand { get; set; }

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
    }
}
