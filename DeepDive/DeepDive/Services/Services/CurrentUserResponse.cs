namespace DeepDive.Services.Services
{
    public class CurrentUserResponse
    {
        public string UserId { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>();
    }
}
