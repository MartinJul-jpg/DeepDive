using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class PackageController : Controller
    {
        private ProductRepository _productRepository;
        private BookingRepository _bookingRepository;

        public PackageController(ProductRepository productRepository, BookingRepository bookingRepository)
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
            Package package = _productRepository.GetPackageById(id);

            return View(package);
        }

        public IActionResult AddPackageToBasket(int id)
        {
            Package package = _productRepository.GetPackageById(id);
            foreach (Product product in package.SetList)
            {
                _bookingRepository.AddProductToBasket(product.ProductId);
            }
            return RedirectToAction("Index", "Basket");
        }
    }
}
