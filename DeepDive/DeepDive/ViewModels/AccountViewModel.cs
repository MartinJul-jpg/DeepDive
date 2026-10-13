using DeepDive.Services;

namespace DeepDive.ViewModels
{
    public class AccountViewModel
    {
        public IIdentityApiService IdentityApiService { get; set; }
        public LoginModel LoginModel { get; set; }
        public RegisterModel RegisterModel { get; set; }
    }
}
