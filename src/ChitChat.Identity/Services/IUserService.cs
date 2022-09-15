namespace ChitChat.Identity.Services;

public interface IUserService
{
    Task CreateUserAysnc(User user);
    Task<List<UserViewModel>> GetAllUsersAsync();
    Task<UserViewModel> GetUserAsync(UserSignInRequest user);
    Task<UserViewModel> GetUserByIdAsync(string id);
    Task<UserViewModel> GetUserByNameAsync(string name);
    Task<UserViewModel> GetUserByEmailAsync(string email);
    Task UpdateUserAsync(UserUpdateRequest user);
    Task DeleteUserByIdAsync(string id);
}