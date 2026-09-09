using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class PackageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show(int id)
        {
            Package package = Repository.ProductRepository.GetPackageById(id);

            return View(package);
        }

        public IActionResult AddPackageToBasket(int id)
        {
            Package package = ProductRepository.GetPackageById(id);
            foreach (Product product in package.SetList)
            {
                BookingRepository.AddProductToBasket(product.ProductId);
            }
            return RedirectToAction("Index", "Basket");
        }
    }
}
