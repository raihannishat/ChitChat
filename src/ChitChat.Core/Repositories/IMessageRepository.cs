using ChitChat.Core.Documents;
using ChitChat.Core.BusinessObjects;
using ChitChat.Data.Repositories;

namespace ChitChat.Core.Repositories;

public interface IMessageRepository : IRepository<Documents.Message>
{
    Task<Documents.Message> GetMessage(string id);
}
