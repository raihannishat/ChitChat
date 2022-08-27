namespace ChitChat.Infrastructure.Documents;

[BsonCollection("Messages")]
public class Message : Document
{
    public string SenderId { get; set; } = string.Empty;
    public string SenderUsername { get; set; } = string.Empty;
    public string RecipientId { get; set; } = string.Empty;
    public string RecipientUsername { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime MessageSent { get; set; } = DateTime.UtcNow;
}
