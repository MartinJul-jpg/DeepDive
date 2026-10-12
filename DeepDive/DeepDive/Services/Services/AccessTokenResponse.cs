namespace DeepDive.Services.Services
{
    // Svaret fra Identity API'ets /login endpoint.
    public class AccessTokenResponse
    {
        public string TokenType { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public long ExpiresIn { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
}
