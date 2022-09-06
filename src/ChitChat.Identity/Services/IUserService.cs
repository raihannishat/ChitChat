namespace ChitChat.Identity.Services;

public interface IUserService
{
    Task CreateUserAysnc(User user);
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserAsync(UserSignInDTO user);
    Task<User> GetUserByIdAsync(string id);
    Task<User> GetUserByNameAsync(string name);
    Task<User> GetUserByEmailAsync(string email);
    Task UpdateUserAsync(User user);
    Task DeleteUserByIdAsync(string id);
}