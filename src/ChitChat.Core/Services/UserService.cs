using ChitChat.Core.Documents;
using ChitChat.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Core.Services
{
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
}
