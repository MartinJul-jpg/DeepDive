using Microsoft.IdentityModel.Tokens;

namespace DeepDive.Services.Services
{
    public class AuthState
    {
        private const string AccessTokenKey = "Auth.AccessToken";
        private const string EmailKey = "Auth.Email";
        private const string UserIdKey = "Auth.UserId";
        private const string RolesKey = "Auth.Roles";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthState(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession? Session { get { return _httpContextAccessor.HttpContext?.Session; } }

        public string? AccessToken { get { return Session?.GetString(AccessTokenKey); } }
        public string? Email { get { return Session?.GetString(EmailKey); } }
        public string? UserId { get { return Session?.GetString(UserIdKey); } }
        public List<string>? Roles { get { return Session?.GetString(RolesKey)?.Split(',').ToList(); } }
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

        public void AddUserInfoToState(string userId, List<string> roles)
        {
            ISession? session = Session;

            if (session == null)
            {
                throw new InvalidOperationException("No active session was found? ");
            }

            session.SetString(UserIdKey, userId);
            if (roles.IsNullOrEmpty())
            {
                session.SetString(RolesKey, string.Empty);
            }
            else
            {
                string roless = string.Join(',', roles);

                session.SetString(RolesKey, roless);
            }
        }

        public void SignOut()
        {
            ISession? session = Session;

            if (session != null)
            {
                session.Remove(EmailKey);
                session.Remove(AccessTokenKey);
                session.Remove(UserIdKey);
                session.Remove(RolesKey);
            }
        }
    }
}
