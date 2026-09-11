using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class ProductController : Controller
    {
        private BasketRepository _basketRepository;
        private ProductRepository _productRepository;
        private BookingRepository _bookingRepository;

        public ProductController(BasketRepository basketRepository, ProductRepository productRepository, BookingRepository bookingRepository)
        {
            _basketRepository = basketRepository;
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
            _productRepository.AddProductToBasket(id);
            return RedirectToAction("Index", "Basket");
        }
    }
}
