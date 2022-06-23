namespace ChitChat.Identity.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetAllUsersAsync() =>
       await _userRepository.GetAll();

    public async Task CreateUser(User user) =>
        await _userRepository.InsertOneAsync(user);
}
