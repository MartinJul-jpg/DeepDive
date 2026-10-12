using DeepDive.Services.Services;
using DeepDive.ViewModels;

namespace DeepDive.Services
{
    public class IdentityApiService
    {
        private readonly AuthState _authState;
        private readonly HttpClient _http;

        private LoginModel _loginModel = new();
        private string? _errorMessage;
        private bool _isBusy;

        public IdentityApiService(HttpClient httpClient, AuthState authState)
        {
            _http = httpClient;
            _authState = authState;
        }

        public async Task HandleLoginAsync(LoginModel loginModel)
        {
            _loginModel = loginModel;
            _isBusy = true;
            _errorMessage = null;

            try
            {
                var response = await _http.PostAsJsonAsync("https://localhost:7126/login", _loginModel);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadFromJsonAsync<AccessTokenResponse>();

                    if (string.IsNullOrEmpty(token?.AccessToken))
                    {
                        _errorMessage = "API'et svarede, men returnerede intet access token.";
                        return;
                    }

                    _authState.SignIn(_loginModel.Email, token.AccessToken);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    _errorMessage = "Forkert e-mail eller adgangskode.";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _errorMessage = "API'et har intet /login endpoint (404).";
                }
                else
                {
                    _errorMessage = $"Login fejlede ({(int)response.StatusCode} {response.ReasonPhrase}).";
                }
            }
            catch (HttpRequestException)
            {
                _errorMessage = "Kunne ikke forbinde til API’et.";
            }
            finally
            {
                _isBusy = false;
            }
        }
    }
}
