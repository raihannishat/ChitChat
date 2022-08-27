namespace ChitChat.Identity.Repositories;

public class UserRepository : MongoRepository<User>, IUserRepository
{
    public UserRepository(IMongoDbSettings settings) : base(settings)
    {

    }
}