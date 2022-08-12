using ChitChat.Core.Documents;
using ChitChat.Data.Configurations;
using ChitChat.Data.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ChitChat.Core.Repositories;

public class MessageRepository : MongoRepository<Documents.Message> , IMessageRepository
{
    public MessageRepository(IMongoDbSettings settings) : base(settings) { }


    public async Task<Message> GetMessage(string id)
    {
        return await _collection.AsQueryable()
            .SingleOrDefaultAsync(x => x.Id == id);
    }
}
