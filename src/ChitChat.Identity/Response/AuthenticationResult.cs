namespace ChitChat.Identity.Response;

public class AuthenticationResult
{
    public string Token { get; set; } = null!;
    public bool Success { get; set; }
    public string RefreshToken { get; set; } = null!;
    public IEnumerable<string> Errors { get; set; } = null!;
}
