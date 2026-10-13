namespace DeepDive.Services.Services
{
    public class AuthState
    {
        private const string AccessTokenKey = "Auth.AccessToken";
        private const string EmailKey = "Auth.Email";
        private const string UserIdKey = "Auth.UserId";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthState(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession? Session { get { return _httpContextAccessor.HttpContext?.Session; } }

        public string? AccessToken { get { return Session?.GetString(AccessTokenKey); } }
        public string? Email { get { return Session?.GetString(EmailKey); } }
        public string? UserId { get { return Session?.GetString(UserIdKey); } }
        public bool IsLoggedIn { get { return !string.IsNullOrWhiteSpace(AccessToken); } }

        public void SignIn(string email, string accessToken)
        {
            ISession? session = Session;

            if (session == null)
            {
                throw new InvalidOperationException("No active session was found? ");
            }

            session.SetString(EmailKey, email);
            session.SetString(AccessTokenKey, accessToken);
        }

        public void AddUserIdToSession(string userId)
        {
            ISession? session = Session;

            if (session == null)
            {
                throw new InvalidOperationException("No active session was found? ");
            }

            session.SetString(UserIdKey, userId);
        }

        public void SignOut()
        {
            ISession? session = Session;

            if (session != null)
            {
                session.Remove(EmailKey);
                session.Remove(AccessTokenKey);
                session.Remove(UserIdKey);
            }
        }
    }
}
