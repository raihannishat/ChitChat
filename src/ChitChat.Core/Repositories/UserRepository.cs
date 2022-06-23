using ChitChat.Core.Documents;
using ChitChat.Data;
using ChitChat.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Core.Repositories
{
    public class UserRepository : MongoRepository<User>, IUserRepository
    {
        public UserRepository(IMongoDbSettings settings) : base(settings)
        {
        }
    }
}
