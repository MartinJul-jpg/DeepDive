using DeepDive.Services;
using DeepDive.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityApiService _identityApiService;

        public AccountController(IIdentityApiService identityApiService)
        {
            _identityApiService = identityApiService;
        }

        public IActionResult Login()
        {
            AccountViewModel accountViewModel = new AccountViewModel() { 
                IdentityApiService = _identityApiService, 
                LoginModel = new LoginModel() };

            return View(accountViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            await _identityApiService.HandleLoginAsync(loginModel);

            if (_identityApiService.ErrorMessage == null)
            {
                return RedirectToAction("Index", "Home");
            }

            AccountViewModel accountViewModel = new AccountViewModel() { IdentityApiService = _identityApiService, LoginModel = loginModel };
            return View(accountViewModel);
        }

        public IActionResult Logout()
        {
            _identityApiService.Logout();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            AccountViewModel accountViewModel = new AccountViewModel() { 
                IdentityApiService = _identityApiService, 
                RegisterModel = new RegisterModel() };

            return View(accountViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            await _identityApiService.HandleRegisterAsync(registerModel);

            if (_identityApiService.SuccessMessage != null)
            {
                return RedirectToAction("Login", "Account");
            }

            AccountViewModel accountViewModel = new AccountViewModel() { IdentityApiService = _identityApiService, RegisterModel = registerModel };
            return View(accountViewModel);
        }
    }
}
