namespace DeepDive.Models
{
    public class SearchQuery
    {
        public string Query { get; set; }
        public List<Product>? Products { get; set; }
        public List<Package>? Packages { get; set; }
    }
}
