using ChitChat.Data.Configurations;
using ChitChat.Data.Documents;
using ChitChat.Identity.Documents;

namespace ChitChat.Infrastructure.Documents;

[BsonCollection("Messages")]
public class Message : Document
{
    public string SenderId { get; set; }
    public string SenderUsername { get; set; }
    public string RecipientId { get; set; }
    public string RecipientUsername { get; set; }
    public string Content { get; set; }
    public DateTime MessageSent { get; set; } = DateTime.UtcNow;
   
}
