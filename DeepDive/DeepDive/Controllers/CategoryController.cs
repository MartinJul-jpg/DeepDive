using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show(int id)
        {
            return View();
        }
    }
}
