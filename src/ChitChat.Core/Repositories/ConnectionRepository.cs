using ChitChat.Infrastructure.Documents;
using ChitChat.Data.Configurations;
using ChitChat.Data.Repositories;

namespace ChitChat.Infrastructure.Repositories;

public class ConnectionRepository : MongoRepository<Documents.Connection> , IConnectionRepository
{
    public ConnectionRepository(IMongoDbSettings settings) : base(settings) { }

}
