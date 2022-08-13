using ChitChat.Identity.Documents;
using System.Text.Json.Serialization;

namespace ChitChat.Core.BusinessObjects;

public class MessageBusinessObject
{
	public string Id { get; set; }
	public int SenderId { get; set; }
	public string SenderName { get; set; }
	public string SenderPhotoUrl { get; set; }
	public int RecipientId { get; set; }
	public string RecipientName { get; set; }
	public string RecipientPhotoUrl { get; set; }
	public string Content { get; set; }
	public DateTime DateRead { get; set; }
	public DateTime MessageSent { get; set; }
	public User Sender { get; set; }
	public User Recipient { get; set; }

	[JsonIgnore]
	public bool SenderDeleted { get; set; }

	[JsonIgnore]
	public bool RecipientDeleted { get; set; }
}
