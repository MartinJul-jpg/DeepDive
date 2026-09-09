namespace DeepDive.Models
{
    public abstract class Package
    {
        public int PackageId { get; set; }
        public List<Product> setList { get;set } = new List<Product>();

        public List<Package_Product> Package_Products { get; set; } = new List<Package_Product>();

        public string GetInfo()
        {
            string result = string.Empty;
            

            foreach (Product product in setList)
            {
                result += $"{product.GetProductInfo()}{"\n"}";
            }

            return result;
        }
    }
}
