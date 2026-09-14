namespace DeepDive.Models
{
    public class Package
    {
        public int PackageId { get; set; }
        public List<Package_Product> Package_Products { get; set; } = new List<Package_Product>();

        public string GetInfo()
        {
            string result = string.Empty;
            

            foreach (Package_Product pp in Package_Products)
            {
                result += $"{pp.Product.GetProductInfo()}{"\n"}";
            }

            return result;
        }
    }
}
