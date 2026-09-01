namespace DeepDive.Models
{
    public abstract class Package
    {
        public int PackageId { get; set; }
        public List<Product> setList = new List<Product>();

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
