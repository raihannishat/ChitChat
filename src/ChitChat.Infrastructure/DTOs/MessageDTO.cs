namespace ChitChat.Infrastructure.DTOs;

public class MessageDTO
{
	public string SenderId { get; set; } = string.Empty;
	public string SenderUsername { get; set; } = string.Empty;
	public string SenderPhotoUrl { get; set; } = string.Empty;
	public string RecipientId { get; set; } = string.Empty;
	public string RecipientUsername { get; set; } = string.Empty;
	public string RecipientPhotoUrl { get; set; } = string.Empty;
	public string Content { get; set; } = string.Empty;
	public DateTime DateRead { get; set; }
	public DateTime MessageSent { get; set; }
}
