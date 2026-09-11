using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class PackageController : Controller
    {
        private BasketRepository _basketRepository;
        private ProductRepository _productRepository;
        private BookingRepository _bookingRepository;

        public PackageController(BasketRepository basketRepository, ProductRepository productRepository, BookingRepository bookingRepository)
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
            Package package = _productRepository.GetPackageById(id);

            return View(package);
        }

        public IActionResult AddPackageToBasket(int id)
        {
            Package package = _productRepository.GetPackageById(id);
            foreach (Package_Product pp in package.Package_Products)
            {
                _productRepository.AddProductToBasket(pp.Product.ProductId);
            }
            return RedirectToAction("Index", "Basket");
        }
    }
}
