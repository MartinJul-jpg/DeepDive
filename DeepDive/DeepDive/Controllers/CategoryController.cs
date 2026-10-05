using DeepDive.Models;
using DeepDive.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IDiveDeepApiService _diveDeepApiService;

        public CategoryController(IDiveDeepApiService diveDeepApiService)
        {
            _diveDeepApiService = diveDeepApiService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Show(string id)
        {
            List<Product> repoList = new List<Product>(await _diveDeepApiService.GetAllProductAsync());
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
