namespace ChitChat.Identity.Response;

public class AuthFailedResponse
{
    public IEnumerable<string> Errors { get; set; } = null!;
}
