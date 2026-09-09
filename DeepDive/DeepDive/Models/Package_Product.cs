namespace DeepDive.Models
{
    public class Package_Product
    {
        public int PackageId { get; set; }
        public Package Package { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
