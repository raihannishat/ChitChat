using ChitChat.Core.Documents;
using ChitChat.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Core.Repositories
{
    public interface IUserRepository : IMongoRepository<User>
    {
    }
}
