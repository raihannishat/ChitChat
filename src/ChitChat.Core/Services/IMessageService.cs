using ChitChat.Infrastructure.DTOs;
using ChitChat.Infrastructure.Documents;

namespace ChitChat.Infrastructure.Services;
public interface IMessageService
{
	//void AddGroup(Group group);
	//void ReplaceGroup(Group group);
	Task AddMessage(MessageDTO message);
	//void AddConnection(Connection connection);
	void DeleteMessage(Message message);
	//Task<Connection> GetConnection(string connectionId);
	//Task<Group> GetGroupForConnection(string connectionId);
	Task<Message> GetMessage(string id);
	//Task<Group> GetMessageGroup(string groupName);
	Task<IEnumerable<MessageDTO>> GetMessageThread(string currentUsername, string recipientUsername);
	//void RemoveConnection(Connection connection);
}