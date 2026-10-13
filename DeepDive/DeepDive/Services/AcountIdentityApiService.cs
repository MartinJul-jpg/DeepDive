using DeepDive.ViewModels;
using static System.Net.WebRequestMethods;

namespace DeepDive.Services
{
    public class AcountIdentityApiService : IAcountIdentityApiService
    {
        private readonly HttpClient _httpClient;
        public AcountIdentityApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task HandleRegisterAsync()
        {
            RegisterViewModel registerViewModel = new();
            try
            {
                using var response = await _httpClient.PostAsJsonAsync("https://localhost:7142/register", new
                {
                    registerViewModel.Email,
                    registerViewModel.Password
                });
                // TO DO: Add correct error responses
                if (response.IsSuccessStatusCode)
                {
                    registerViewModel = new();

                    await Task.Delay(1500);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {

                }
                else
                {
                    //errorMessage = $"Registrering fejlede ({(int)response.StatusCode} {response.ReasonPhrase}).";
                }
            }
            catch (HttpRequestException)
            {

            }
            
        }

        public Task HandleLoginAsync()
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }
    }
}
