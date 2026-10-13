using DeepDive.Services;
using DeepDive.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDiveDeepApiService _diveDeepApiService;

        public AccountController(IDiveDeepApiService diveDeepApiService)
        {
            _diveDeepApiService = diveDeepApiService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            var loginmodel = new LoginViewModel();

            return View(loginmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            using var response = 
        }
    }
}
