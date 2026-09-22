using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class CategoryController : Controller
    {
        private ProductRepository _productRepository;

        public CategoryController(BasketRepository basketRepository, ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show(string id)
        {
            List<Product> repoList = new List<Product>(_productRepository.GetAllProducts());
            List<Product> resultList = new List<Product>();

            foreach (Product product in repoList)
            {
                if (string.Equals(product.GetType().Name, id)) 
                {
                    resultList.Add(product);
                }
            }

            return View(resultList);
        }
    }
}
