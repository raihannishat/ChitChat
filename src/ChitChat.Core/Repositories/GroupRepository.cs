using ChitChat.Core.Documents;
using ChitChat.Data.Configurations;
using ChitChat.Data.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ChitChat.Core.Repositories;

public class GroupRepository : MongoRepository<Documents.Group> , IGroupRepository
{
    public GroupRepository(IMongoDbSettings settings) : base(settings) { }
    public async Task<Group> FindGroupForConnection(string connectionId)
    {
        return await _collection.AsQueryable()
                       .Where(c => c.Connections.Any(x => x.ConnectionId == connectionId))
                       .FirstOrDefaultAsync();
        
    }

    public async Task<Group> FindMessageGroup(string groupName)
    {
        return await _collection.AsQueryable()
            .FirstOrDefaultAsync(x => x.Name == groupName);
    }

}
