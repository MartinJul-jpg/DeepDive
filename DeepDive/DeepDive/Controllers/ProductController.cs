using DeepDive.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show(int id)
        {
            Product product = Repository.ProductRepository.GetProductById(id);

            return View(product);
        }
    }
}
