using Microsoft.AspNetCore.Mvc;

namespace DiveDeepAPI.Controllers
{
    [ApiController]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
