namespace ChitChat.Identity.Documents;

public class RefreshToken : Document
{
    public string Token { get; set; } = String.Empty;
    public string JwtId { get; set; } = String.Empty;
    public DateTime CreationDate { get; set; }
    public DateTime ExpiryDate { get; set; } 
    public bool Used { get; set; }
    public bool Invalidate { get; set; }
    public string UserId { get; set; } = String.Empty;
}
