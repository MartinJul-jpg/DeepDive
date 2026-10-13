using DeepDive.Services;

namespace DeepDive.ViewModels
{
    public class AccountViewModel
    {
        public LoginViewModel LoginViewModel { get; set; }

        public AuthState AuthState { get; set; }
       
    }
}
