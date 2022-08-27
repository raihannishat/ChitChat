namespace ChitChat.Identity.Repositories;

public class AuthRepository : MongoRepository<RefreshToken>, IAuthRepository
{
    public AuthRepository(IMongoDbSettings settings) : base(settings)
    {

    }
}
