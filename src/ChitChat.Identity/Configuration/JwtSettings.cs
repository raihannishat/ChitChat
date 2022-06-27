namespace ChitChat.Identity.Configuration;

public class JwtSettings : IJwtSettings
{
    public string? Secret { get; set; }
}
