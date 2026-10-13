using DeepDive.Services;
using DeepDive.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAcountIdentityApiService _acountIdentityApiService;

        public AccountController(IAcountIdentityApiService acountIdentityApiService)
        {
            _acountIdentityApiService = acountIdentityApiService;
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
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, AuthState authState)
        {
            
            var request = await _acountIdentityApiService.HandleLoginAsync(loginViewModel, authState);

            if (request == null)
            {
                NotFound();
            }

            return View(request);
        }
    }
}
