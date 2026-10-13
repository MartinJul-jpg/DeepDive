using DeepDive.ViewModels;

namespace DeepDive.Services
{
    public interface IAcountIdentityApiService
    {
        Task<AccountViewModel> HandleLoginAsync(LoginViewModel loginViewModel, AuthState authState );
        Task Logout(AuthState authState);

        Task HandleRegisterAsync();
    }
}
