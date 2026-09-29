using DeepDive.Data;
using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class PackageController : Controller
    {
        private CartRepository _cartRepository;
        private ProductRepository _productRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public PackageController(CartRepository cartRepository, ProductRepository productRepository, UserManager<ApplicationUser> userManager)
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
            Package package = _productRepository.GetPackageById(id);

            return View(package);
        }

        [Authorize(Roles = "User")]
        public IActionResult AddPackageTocart(int id, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Package package = _productRepository.GetPackageById(id);
                foreach (Package_Product pp in package.Package_Products)
                {
                    _cartRepository.AddProductToCart(pp.ProductId, _userManager.GetUserId(this.User));
                }
            }
            return RedirectToAction("Index", "cart");
        }
    }
}
