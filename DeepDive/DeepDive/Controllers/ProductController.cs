using DeepDive.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Data;
using Shared.Models;
using System.Security.Claims;

namespace DeepDive.Controllers
{
    public class ProductController : Controller
    {
        private string _userId
        {
            get { return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException(); }
        }

        private readonly IDiveDeepApiService _diveDeepApiService;

        public ProductController(IDiveDeepApiService diveDeepApiService)
        {
            _diveDeepApiService = diveDeepApiService;
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
