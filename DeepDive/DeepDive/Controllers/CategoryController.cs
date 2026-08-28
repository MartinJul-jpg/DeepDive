using DeepDive.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show(string id)
        {
            List<Product> repoList = new List<Product>(Repository.ProductRepository.GetAll());
            List<Product> resultList = new List<Product>();

            foreach (Product product in repoList)
            {
                if (string.Equals(product.GetType().Name, id)) 
                {
                    resultList.Add(product);
                }
            }

            return View(resultList);
        }
    }
}
