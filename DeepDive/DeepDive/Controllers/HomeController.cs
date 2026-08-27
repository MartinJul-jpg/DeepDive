using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
