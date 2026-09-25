using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace DeepDive.Controllers
{
    public class SearchController : Controller
    {
        private BasketRepository _basketRepository;
        private ProductRepository _productRepository;

        public SearchController(BasketRepository basketRepository, ProductRepository productRepository)
        {
            _basketRepository = basketRepository;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show(SearchQuery searchQuery)
        {
            string[] queryWords = searchQuery.Query.ToLower().Split(" ");
            if (!searchQuery.Query.IsNullOrEmpty())
            {
                searchQuery.Packages = _productRepository.GetAllPackages()
                    //.Where(package => Regex.Match(package.GetSearchBlob().ToLower(), $"{searchQuery.Query.ToLower()}+").Length > 0)
                    .Where(package => HasQueryWords(package.GetSearchBlob(), queryWords))
                    .ToList();
                searchQuery.Products = _productRepository.GetAllProducts()
                    //.Where(product => Regex.Match(product.GetSearchBlob().ToLower(), $"{searchQuery.Query.ToLower()}+").Length > 0)
                    .Where(product => HasQueryWords(product.GetSearchBlob(), queryWords))
                    .ToList();

                bool HasQueryWords(string blob, string[] queryWords)
                {
                    string properBlob = blob.ToLower();
                    return queryWords.All(queryWord => properBlob.Contains(queryWord));
                }
            }
            else
            {
                searchQuery.Packages = new List<Package>();
                searchQuery.Products = new List<Product>();
            }

            return View(searchQuery);
        }
    }
}
