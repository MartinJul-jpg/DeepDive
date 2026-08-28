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
            List<Product> repoList = new List<Product>(Repository.ProductRepository.GetAll());
            Product product = repoList.Find(product => product.ProductId == id);

            return View(product);
        }
    }
}
