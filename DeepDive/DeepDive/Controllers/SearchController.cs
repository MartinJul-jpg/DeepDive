using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace DeepDive.Controllers
{
    public class SearchController : Controller
    {
        private CartRepository _cartRepository;
        private ProductRepository _productRepository;

        public SearchController(CartRepository cartRepository, ProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        [HttpPost]
        public IActionResult Show(string searchQuery)
        {
            List<Package> packages;
            List<Product> products;
            if (!searchQuery.IsNullOrEmpty())
            {
                string[] queryWords = searchQuery.ToLower().Split(" ");

                packages = _productRepository.GetAllPackages()
                    //.Where(package => Regex.Match(package.GetSearchBlob().ToLower(), $"{searchQuery.Query.ToLower()}+").Length > 0)
                    .Where(package => HasQueryWords(package.GetSearchBlob(), queryWords))
                    .ToList();
                products = _productRepository.GetAllProducts()
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
                packages = new List<Package>();
                products = new List<Product>();
            }

            SearchQuery result = new SearchQuery { Packages = packages, Products = products };

            return View(result);
        }
    }
}
