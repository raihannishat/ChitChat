namespace ChitChat.Infrastructure.Repositories;

public class MessageRepository : MongoRepository<Message>, IMessageRepository
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

	public async Task<IList<MessageDTO>>
		GetMessageThread(string currentUsername, string recipientUsername)
	{
		var messages = await _collection.AsQueryable()
			.Where(m => (m.RecipientUsername == currentUsername
					&& m.SenderUsername == recipientUsername)
					|| (m.RecipientUsername == recipientUsername
					&& m.SenderUsername == currentUsername)
			)
			.OrderBy(m => m.MessageSent)
			//.ProjectTo<DTOs.Message>(_mapper.ConfigurationProvider)
			.ToListAsync();

		return (_mapper.Map<IList<MessageDTO>>(messages));
	}
}
