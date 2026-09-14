using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class CategoryController : Controller
    {
        private BookingRepository _bookingRepository;
        private ProductRepository _productRepository;

        public CategoryController(BookingRepository bookingRepository, ProductRepository productRepository)
        {
            _bookingRepository = bookingRepository;
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
