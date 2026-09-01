using DeepDive.Models;
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
    }
}
