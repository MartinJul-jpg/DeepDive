namespace DeepDive.Services
{
    public interface IAcountIdentityApiService
    {
        Task HandleLoginAsync();
        Task Logout();

        Task HandleRegisterAsync();
    }
}
