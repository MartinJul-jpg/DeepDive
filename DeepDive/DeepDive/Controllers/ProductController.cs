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
        private CartRepository _cartRepository;
        private ProductRepository _productRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(CartRepository cartRepository, ProductRepository productRepository, UserManager<ApplicationUser> userManager)
        {
            _cartRepository = cartRepository;
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
        public IActionResult AddTocart(int id, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                _cartRepository.AddProductToCart(id, _userManager.GetUserId(this.User));
            }
            return RedirectToAction("Index", "cart");
        }
    }
}
