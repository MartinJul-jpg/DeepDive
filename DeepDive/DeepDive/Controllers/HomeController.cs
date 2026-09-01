using DeepDive.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Package> packages =  new List<Package>(Repository.ProductRepository.GetAllPackages());

            return View(packages);
        }
    }
}
