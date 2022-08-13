using System.Text.Json.Serialization;

namespace ChitChat.Core.BusinessObjects;

public class CreateMessageBusinessObject
{
	public string RecipientUsername { get; set; }
	public string Content { get; set; }
}
