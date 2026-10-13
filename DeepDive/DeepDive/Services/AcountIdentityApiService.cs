using DeepDive.ViewModels;
using Microsoft.AspNetCore.Authentication.BearerToken;
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

        public async Task<AccountViewModel> HandleLoginAsync(LoginViewModel loginViewModel, AuthState authState)
        {
            
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7142/login", loginViewModel);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadFromJsonAsync<AccessTokenResponse>();

                    if (string.IsNullOrEmpty(token?.AccessToken))
                    {
                        //errorMessage = "API'et svarede, men returnerede intet access token.";

                    }

                    authState.SignIn(loginViewModel.Email, token.AccessToken);
                    return new AccountViewModel()
                    {
                        LoginViewModel = loginViewModel,
                        AuthState = authState

                    };
                    
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    //errorMessage = "Forkert e-mail eller adgangskode.";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    //errorMessage = "API'et har intet /login endpoint (404).";
                }
                else
                {
                    //errorMessage = $"Login fejlede ({(int)response.StatusCode} {response.ReasonPhrase}).";
                }
            }
            catch (HttpRequestException)
            {
                //errorMessage = "Kunne ikke forbinde til API’et.";
            }

            return null;
        }
        

        public async Task Logout(AuthState authState)
        {
            authState.SignOut();
        }
    }
}
