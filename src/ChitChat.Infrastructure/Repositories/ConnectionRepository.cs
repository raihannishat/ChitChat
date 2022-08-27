namespace ChitChat.Infrastructure.Repositories;

public class ConnectionRepository : MongoRepository<Connection>, IConnectionRepository
{
    public ConnectionRepository(IMongoDbSettings settings) : base(settings)
    {

    }
}
