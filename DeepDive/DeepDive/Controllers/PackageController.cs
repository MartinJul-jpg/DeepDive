using DeepDive.Data;
using DeepDive.Models;
using DeepDive.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class PackageController : Controller
    {
        private readonly IDiveDeepApiService _diveDeepApiService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PackageController(IDiveDeepApiService diveDeepApiService, UserManager<ApplicationUser> userManager)
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
            Package package = await _diveDeepApiService.GetPackageByIdAsync(id);

            return View(package);
        }

        //[Authorize(Roles = "User")]
        public async Task<IActionResult> AddPackageTocart(int id, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Package package = await _diveDeepApiService.GetPackageByIdAsync(id);
                foreach (Package_Product pp in package.Package_Products)
                {
                    await _diveDeepApiService.AddProductToCartAsync(pp.ProductId, _userManager.GetUserId(this.User));
                }
            }
            return RedirectToAction("Index", "cart");
        }
    }
}
