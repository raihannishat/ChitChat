using ChitChat.Core.BusinessObjects;
using ChitChat.Core.Documents;

namespace ChitChat.Core.Services;
public interface IMessageService
{
	void AddGroup(Group group);
	void ReplaceGroup(Group group);
	void AddMessage(MessageBusinessObject message);
	void AddConnection(Connection connection);
	void DeleteMessage(Message message);
	Task<Connection> GetConnection(string connectionId);
	Task<Group> GetGroupForConnection(string connectionId);
	Task<Message> GetMessage(string id);
	Task<Group> GetMessageGroup(string groupName);
	Task<IEnumerable<MessageBusinessObject>> GetMessageThread(string currentUsername, string recipientUsername);
	void RemoveConnection(Connection connection);
}