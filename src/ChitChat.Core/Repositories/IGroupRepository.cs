using ChitChat.Core.Documents;
using ChitChat.Data.Repositories;

namespace ChitChat.Core.Repositories;

public interface IGroupRepository : IRepository<Group>
{
    Task<Group> FindGroupForConnection(string connectionId);

    Task<Group> FindMessageGroup(string groupName)

}