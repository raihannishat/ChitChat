namespace ChitChat.Identity.Services;

public interface IUserService
{
    Task CreateUser(User user);
    Task<List<User>> GetAllUsersAsync();
}