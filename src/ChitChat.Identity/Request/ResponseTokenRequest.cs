namespace ChitChat.Identity.Request;

public class ResponseTokenRequest
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
