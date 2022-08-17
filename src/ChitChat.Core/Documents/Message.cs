using ChitChat.Data.Configurations;
using ChitChat.Data.Documents;
using ChitChat.Identity.Documents;

namespace ChitChat.Core.Documents;

[BsonCollection("Messages")]
public class Message : Document
{
    public int SenderId { get; set; }
    public string SenderUsername { get; set; }
    public User Sender { get; set; }
    public int RecipientId { get; set; }
    public string RecipientUsername { get; set; }
    public User Recipient { get; set; }
    public string Content { get; set; }
    public DateTime DateRead { get; set; }
    public DateTime MessageSent { get; set; } = DateTime.UtcNow;
    public bool SenderDeleted { get; set; }
    public bool RecipientDeleted { get; set; }
}
