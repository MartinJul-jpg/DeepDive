using Shared.Models;

namespace DeepDive.ViewModels
{
    public class SearchQuery
    {
        public List<Product>? Products { get; set; }
        public List<Package>? Packages { get; set; }
    }
}
