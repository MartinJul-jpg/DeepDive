using DeepDive.ViewModels;

namespace DeepDive.Services
{
    public interface IIdentityApiService
    {
        public string? ErrorMessage { get; }
        public bool IsBusy { get; }
        public string? SuccessMessage { get; }
        public List<string> ApiErrors { get; }

        Task HandleLoginAsync(LoginModel loginModel);
        void Logout();
        Task HandleRegisterAsync(RegisterModel registerModel);

    }
}
