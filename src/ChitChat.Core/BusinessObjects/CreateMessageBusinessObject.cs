using System.Text.Json.Serialization;

namespace ChitChat.Core.BusinessObjects;

public class CreateMessageBusinessObject
{
	public string Sender { get; set; }
	public string Receiver { get; set; }
	public string Content { get; set; }
}
