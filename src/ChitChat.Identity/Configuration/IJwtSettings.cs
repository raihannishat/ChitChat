namespace ChitChat.Identity.Configuration;

public interface IJwtSettings
{
    string? Secret { get; set; }
}