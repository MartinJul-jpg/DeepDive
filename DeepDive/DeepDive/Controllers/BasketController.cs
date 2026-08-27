using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
