namespace ChitChat.Infrastructure.DTOs;

public class CreateMessageDTO
{
	public string Sender { get; set; } = string.Empty;
	public string Receiver { get; set; } = string.Empty;
	public string Content { get; set; } = string.Empty;
}
