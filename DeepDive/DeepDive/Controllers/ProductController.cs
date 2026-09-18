using DeepDive.Data;
using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class ProductController : Controller
    {
        private BasketRepository _basketRepository;
        private ProductRepository _productRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(BasketRepository basketRepository, ProductRepository productRepository, UserManager<ApplicationUser> userManager)
        {
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show(int id)
        {
            Product product = _productRepository.GetProductById(id);

            return View(product);
        }

        [Authorize(Roles = "User")]
        public IActionResult AddToBasket(int id, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                _basketRepository.AddProductToBasket(id, _userManager.GetUserId(this.User));
            }
            return RedirectToAction("Index", "Basket");
        }
    }
}
