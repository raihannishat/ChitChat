using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChitChat.Core.BusinessObjects;
using ChitChat.Core.Documents;
using ChitChat.Data.Configurations;
using ChitChat.Data.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ChitChat.Core.Repositories;

public class MessageRepository : MongoRepository<Documents.Message> , IMessageRepository
{
	private readonly IMapper _mapper;

	public MessageRepository(IMongoDbSettings settings, IMapper mapper) : base(settings) 
	{
		_mapper = mapper;
	}


    public async Task<Message> GetMessage(string id)
    {
        return await _collection.AsQueryable()
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IList<BusinessObjects.MessageBusinessObject>>
		GetMessageThread(string currentUsername, string recipientUsername)
    {
		var messages = await _collection.AsQueryable()
			.Where(m => (m.RecipientUsername == currentUsername
					&& m.SenderUsername == recipientUsername )
					|| (m.RecipientUsername == recipientUsername
					&& m.SenderUsername == currentUsername )
			)
			.OrderBy(m => m.MessageSent)
			//.ProjectTo<BusinessObjects.Message>(_mapper.ConfigurationProvider)
			.ToListAsync();

		return ( _mapper.Map<IList<MessageBusinessObject>>(messages));
	}
}
