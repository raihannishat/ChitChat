using ChitChat.Core.Documents;
using ChitChat.Core.BusinessObjects;
using ChitChat.Data.Repositories;

namespace ChitChat.Core.Repositories;

public interface IMessageRepository : IRepository<Documents.Message>
{
    Task<Message> GetMessage(string id);
	Task<IEnumerable<MessageBusinessObject>> GetMessageThread(string currentUsername,
		string recipientUsername);
}
