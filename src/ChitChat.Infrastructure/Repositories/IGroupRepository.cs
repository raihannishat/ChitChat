using ChitChat.Infrastructure.Documents;
using ChitChat.Data.Repositories;

namespace ChitChat.Infrastructure.Repositories;

public interface IGroupRepository : IRepository<Group>
{
    Task<Group> FindGroupForConnection(string connectionId);

    Task<Group> FindMessageGroup(string groupName);
}