using ChitChat.Core.Documents;

namespace ChitChat.Core.Services
{
    public interface IUserService
    {
        Task CreateUser(User user);
        Task<List<User>> GetAllUsersAsync();
    }
}