namespace DeepDive.Models
{
    public abstract class Product
    {
        public int ProductId { get; set; }
        public double Price { get; set; }
        public string Brand { get; set; }

        public abstract string GetUniqueInfo();

        public string GetProductInfo() 
        {
            return $"Price: {Price}{"\n"} Brand: {Brand}{"\n"}";
        }
    }
}
