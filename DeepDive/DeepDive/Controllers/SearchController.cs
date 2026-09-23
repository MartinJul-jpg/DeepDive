using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

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
            searchQuery.Packages = _productRepository.GetAllPackages();
            searchQuery.Products = _productRepository.GetAllProducts();

            return View(searchQuery);
        }
    }
}
