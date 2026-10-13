using DeepDive.Services;
using DeepDive.Services.AuthServices;
using DeepDive.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DeepDive.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityApiService _identityApiService;
        private readonly AuthState _authState;

        public AccountController(IIdentityApiService identityApiService, AuthState authState)
        {
            _identityApiService = identityApiService;
            _authState = authState;
        }

        public IActionResult Login()
        {
            AccountViewModel accountViewModel = new AccountViewModel() { 
                IdentityApiService = _identityApiService, 
                LoginModel = new LoginModel() };

            return View(accountViewModel);
        }

        [HttpPost]
        //forms defining method="post", asp-controller, asp-action and using taghelpers adds the anti forgery token that would act as a pair with user cookie
        //to prevent cross site submissions using user cookies to submit outrageous form contents using the users cookie identifier. 
        //we should consider putting this on other critical httpposts throughout the webapp. 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            await _identityApiService.HandleLoginAsync(loginModel);

            if (_identityApiService.ErrorMessage == null)
            {
                //now that the accesstoken belonging to the current session having to do with the user and identifying the user when making API calls, 
                //we have the information needed to make and add a cookie for the user 
                List<Claim> claims = new List<Claim>()
                {
                    new(ClaimTypes.NameIdentifier, _authState.UserId!),
                    new(ClaimTypes.Name, _authState.Email!)
                };
                claims.AddRange((_authState.Roles ?? new List<string>())
                    .Select(role => new Claim(ClaimTypes.Role, role)));

                ClaimsIdentity identity = new ClaimsIdentity(
                    claims, 
                    CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(identity));
                //end of cookie stuff

                return RedirectToAction("Index", "Home");
            }

            AccountViewModel accountViewModel = new AccountViewModel() { IdentityApiService = _identityApiService, LoginModel = loginModel };
            return View(accountViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            _identityApiService.Logout();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

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
