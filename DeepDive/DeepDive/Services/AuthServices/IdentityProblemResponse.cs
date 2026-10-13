namespace DeepDive.Services.AuthServices
{
    // Fejlsvaret (ValidationProblem) fra Identity API'ets /register endpoint.
    public class IdentityProblemResponse
    {
        public string? Title { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }
    }
}
