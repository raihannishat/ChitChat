namespace ChitChat.Infrastructure.Repositories;

public interface IMessageRepository : IRepository<Documents.Message>
{
	Task<Message> GetMessage(string id);
	Task<IList<MessageDTO>> GetMessageThread(string currentUsername, string recipientUsername);
}
