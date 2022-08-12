using AutoMapper;
using ChitChat.Core.BusinessObjects;
using ChitChat.Core.Documents;
using ChitChat.Core.Repositories;


namespace ChitChat.Core.Services;
public class MessageService
{
    private readonly IMessageRepository _messageRespository;
    private readonly IGroupRepository _groupRepository;
    private readonly IConnectionRepository _connectionRepository;
    private readonly IMapper _mapper;

    public MessageService(IMessageRepository messageRepository, IGroupRepository groupRepository,
        IConnectionRepository connectionRepository, IMapper mapper)
    {
        _messageRespository = messageRepository;
        _groupRepository = groupRepository;
        _connectionRepository = connectionRepository;
        _mapper = mapper;
    }

    public void AddMessage(BusinessObjects.Message message)
    {
        _messageRespository.InsertOneAsync(_mapper.Map<Documents.Message>(message));
    }

    public async void AddGroup(Group group)
    {
        await _groupRepository.InsertOneAsync(group);
    }

    public async Task<Connection> GetConnection(string connectionId)
    {
        return await _connectionRepository.FindByIdAsync(connectionId);
    }

    public async Task<Group> GetGroupForConnection(string connectionId)
    {
        return await _groupRepository.FindGroupForConnection(connectionId);
    }

    public async Task<Group> GetMessageGroup(string groupName)
    {
        return await _groupRepository.FindMessageGroup(groupName);
    }

    public async void DeleteMessage(Documents.Message message)
    {
        await _messageRespository.DeleteByIdAsync(message.Id);
    }

    public async void RemoveConnection(Documents.Connection connection)
    {
        await _connectionRepository.DeleteByIdAsync(connection.Id);
    }


    public async Task<Documents.Message> GetMessage(string id)
    {
        return await _messageRespository.GetMessage(id);
    }

    public async Task<IEnumerable<BusinessObjects.Message>> GetMessageThread(string currentUsername,
        string recipientUsername)
    {
        var messages = await _context.Messages
            .Include(u => u.Sender).ThenInclude(p => p.Photos)
            .Include(u => u.Recipient).ThenInclude(p => p.Photos)
            .Where(m => m.Recipient.UserName == currentUsername && m.RecipientDeleted == false
                    && m.Sender.UserName == recipientUsername
                    || m.Recipient.UserName == recipientUsername
                    && m.Sender.UserName == currentUsername && m.SenderDeleted == false
            )
            .OrderBy(m => m.MessageSent)
            .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        var unreadMessages = messages.Where(m => m.DateRead == null
            && m.RecipientUsername == currentUsername).ToList();

        if (unreadMessages.Any())
        {
            foreach (var message in unreadMessages)
            {
                message.DateRead = DateTime.UtcNow;
            }
        }

        return messages;
    }
}
