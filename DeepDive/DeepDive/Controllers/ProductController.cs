using DeepDive.Models;
using DeepDive.Repository;
using DeepDive.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class ProductController : Controller
    {
        private ProductRepository _productRepository;
        private BookingRepository _bookingRepository;

        public ProductController(ProductRepository productRepository, BookingRepository bookingRepository)
        {
            _productRepository = productRepository;
            _bookingRepository = bookingRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show(int id)
        {
            Product product = _productRepository.GetProductById(id);

            return View(product);
        }

        public IActionResult AddToBasket(int id)
        {
            _bookingRepository.AddProductToBasket(id);
            return RedirectToAction("Index", "Basket");
        }
    }
}
