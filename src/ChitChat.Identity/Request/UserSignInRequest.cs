namespace ChitChat.Identity.Request;

public class UserSignInRequest
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
