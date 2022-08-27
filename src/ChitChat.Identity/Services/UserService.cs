namespace ChitChat.Identity.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task CreateUserAysnc(User user)
    {
        await _userRepository.InsertOneAsync(user);
    }

    public async Task DeleteUserByIdAsync(string id)
    {
        await _userRepository.DeleteByIdAsync(id);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAll();
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _userRepository.FindOneAsync(user => user.Email == email);
    }

    public async Task<User> GetUserByIdAsync(string id)
    {
        return await _userRepository.FindOneAsync(user => user.Id == id);
    }

    public async Task<User> GetUserByNameAsync(string name)
    {
        return await _userRepository.FindOneAsync(user => user.Name == name);
    }

    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.ReplaceOneAsync(user);
    }
}
