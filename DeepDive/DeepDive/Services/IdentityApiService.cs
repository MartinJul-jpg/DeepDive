using DeepDive.Services.Services;
using DeepDive.ViewModels;

namespace DeepDive.Services
{
    public class IdentityApiService : IIdentityApiService
    {
        private readonly AuthState _authState;
        private readonly HttpClient _http;

        private LoginModel _loginModel = new();
        private string? _errorMessage;
        public string? ErrorMessage { get { return _errorMessage; } }
        private bool _isBusy;
        public bool IsBusy { get { return _isBusy; } }

        private RegisterModel _registerModel = new();
        private string? _successMessage;
        public string? SuccessMessage { get { return _successMessage; } }
        private List<string> _apiErrors = new();
        public List<string> ApiErrors { get { return _apiErrors; } }

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
                var response = await _http.PostAsJsonAsync("login", _loginModel);


                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadFromJsonAsync<AccessTokenResponse>();

                    if (string.IsNullOrEmpty(token?.AccessToken))
                    {
                        _errorMessage = "API'et svarede, men returnerede intet access token.";
                        return;
                    }

                    _authState.SignIn(_loginModel.Email, token.AccessToken);
                    await GetUserInfo();
                    //NavManager.NavigateTo("/golden");
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

        public void Logout()
        {
            _authState.SignOut();
            _loginModel = new();
        }

        public async Task HandleRegisterAsync(RegisterModel registerModel)
        {
            _registerModel = registerModel;
            _isBusy = true;
            _successMessage = null;
            _errorMessage = null;
            _apiErrors.Clear();

            try
            {
                var response = await _http.PostAsJsonAsync("register", new
                {
                    _registerModel.Email,
                    _registerModel.Password
                });

                if (response.IsSuccessStatusCode)
                {
                    _successMessage = "Kontoen blev oprettet! Du sendes videre til login…";
                    _registerModel = new();

                    await Task.Delay(1500);
                    //NavManager.NavigateTo("/login");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    _errorMessage = "Kontoen kunne ikke oprettes:";
                    await ReadApiErrors(response);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _errorMessage = "API'et har intet /register endpoint (404).";
                }
                else
                {
                    _errorMessage = $"Registrering fejlede ({(int)response.StatusCode} {response.ReasonPhrase}).";
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

        private async Task ReadApiErrors(HttpResponseMessage response)
        {
            try
            {
                var problem = await response.Content.ReadFromJsonAsync<IdentityProblemResponse>();
                if (problem?.Errors is not null)
                    _apiErrors.AddRange(problem.Errors.Values.SelectMany(e => e));
            }
            catch (System.Text.Json.JsonException)
            {
                // Svaret var ikke JSON – vis blot den generelle fejlbesked.
            }
        }

        private async Task GetUserInfo()
        {
            try
            {
                var response = await _http.GetAsync("account/me");

                if (response.IsSuccessStatusCode)
                {
                    CurrentUserResponse? currentUserResponse = await response.Content.ReadFromJsonAsync<CurrentUserResponse>();

                    if (currentUserResponse != null)
                    {
                        _authState.AddUserInfoToState(currentUserResponse.UserId, currentUserResponse.Roles);

                        return;
                    }
                    else
                    {
                        _errorMessage = "Hentningen lykkedes men det hentede objekt var tomt? ";
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    _errorMessage = "BrugerId kunne ikke hentes:";
                    await ReadApiErrors(response);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _errorMessage = "API'et har intet /register endpoint (404).";
                }
                else
                {
                    _errorMessage = $"Hentning fejlede ({(int)response.StatusCode} {response.ReasonPhrase}).";
                }
            }
            catch (HttpRequestException)
            {
                _errorMessage = "Kunne ikke forbinde til API’et.";
            }

            //logout such that you dont have a logged in user with no userId
            Logout();
        }
    }
}
