using DeepDive.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Data;
using Shared.Models;
using System.Security.Claims;

namespace DeepDive.Controllers
{
    public class PackageController : Controller
    {
        private string _userId
        {
            get { return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException(); }
        }

        private readonly IDiveDeepApiService _diveDeepApiService;

        public PackageController(IDiveDeepApiService diveDeepApiService)
        {
            _diveDeepApiService = diveDeepApiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Show(int id)
        {
            Package package = await _diveDeepApiService.GetPackageByIdAsync(id);

            return View(package);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddPackageTocart(int id, int amount)
        {
            await _diveDeepApiService.CreateCartAsync(_userId);

            for (int i = 0; i < amount; i++)
            {
                Package package = await _diveDeepApiService.GetPackageByIdAsync(id);
                foreach (Package_Product pp in package.Package_Products)
                {
                    await _diveDeepApiService.AddProductToCartAsync(pp.ProductId, _userId);
                }
            }
            return RedirectToAction("Index", "cart");
        }
    }
}
