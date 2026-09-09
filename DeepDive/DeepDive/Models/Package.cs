namespace DeepDive.Models
{
    public class Package
    {
        public int PackageId { get; set; }
        public List<Product> SetList { get; set; } = new List<Product>();

        public List<Package_Product> Package_Products { get; set; } = new List<Package_Product>();


        
        public string GetInfo()
        {
            string result = string.Empty;
            

            foreach (Product product in SetList)
            {
                result += $"{product.GetProductInfo()}{"\n"}";
            }

            return result;
        }
    }
}
