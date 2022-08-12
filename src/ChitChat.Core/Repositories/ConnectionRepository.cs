using ChitChat.Core.Documents;
using ChitChat.Data.Configurations;
using ChitChat.Data.Repositories;

namespace ChitChat.Core.Repositories;

public class ConnectionRepository : MongoRepository<Documents.Connection> , IConnectionRepository
{
    public ConnectionRepository(IMongoDbSettings settings) : base(settings) { }

}
