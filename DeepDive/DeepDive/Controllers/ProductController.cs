using DeepDive.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Data;
using Shared.Models;

namespace DeepDive.Controllers
{
    public class ProductController : Controller
    {
        private string _userId
        {
            get { return _userManager.GetUserId(this.User); }
        }

        private readonly IDiveDeepApiService _diveDeepApiService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(IDiveDeepApiService diveDeepApiService, UserManager<ApplicationUser> userManager)
        {
            _diveDeepApiService = diveDeepApiService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Show(int id)
        {
            Product product = await _diveDeepApiService.GetProductByIdAsync(id);

            return View(product);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddTocart(int id, int amount)
        {
            await _diveDeepApiService.CreateCartAsync(_userId);

            for (int i = 0; i < amount; i++)
            {
                await _diveDeepApiService.AddProductToCartAsync(id, _userId);
            }
            return RedirectToAction("Index", "cart");
        }
    }
}
