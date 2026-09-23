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
            if (!searchQuery.Query.IsNullOrEmpty())
            {
                searchQuery.Packages = _productRepository.GetAllPackages()
                    .Where(package => Regex.Match(package.GetSearchBlob(), $"{searchQuery.Query}+").Length > 0)
                    .ToList();
                searchQuery.Products = _productRepository.GetAllProducts()
                    .Where(product => Regex.Match(product.GetSearchBlob(), $"{searchQuery.Query}+").Length > 0)
                    .ToList();
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
