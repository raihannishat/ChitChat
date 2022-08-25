namespace ChitChat.Infrastructure.DTOs;

public class MessageDTO
{
	public string SenderId { get; set; }
	public string SenderUsername { get; set; }
	public string SenderPhotoUrl { get; set; }
	public string RecipientId { get; set; }
	public string RecipientUsername { get; set; }
	public string RecipientPhotoUrl { get; set; }
	public string Content { get; set; }
	public DateTime DateRead { get; set; }
	public DateTime MessageSent { get; set; }
}
