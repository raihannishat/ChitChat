using AutoMapper;
using ChitChat.Core.BusinessObjects;
using ChitChat.Core.Documents;
using ChitChat.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace ChitChat.Core.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRespository;
    private readonly IMapper _mapper;
    private readonly ILogger<MessageService> _logger;

    public MessageService(IMessageRepository messageRepository,
        IMapper mapper, ILogger<MessageService> logger)
    {
        _messageRespository = messageRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task AddMessage(MessageBusinessObject message)
    {
        await _messageRespository.InsertOneAsync(_mapper.Map<Message>(message));
    }

    public async void DeleteMessage(Message message)
    {
        await _messageRespository.DeleteByIdAsync(message.Id);
    }

    public async Task<Message> GetMessage(string id)
    {
        return await _messageRespository.GetMessage(id);
    }

    public async Task<IEnumerable<MessageBusinessObject>> GetMessageThread(string currentUsername,
        string recipientUsername)
    {
        return await _messageRespository.GetMessageThread(currentUsername, recipientUsername);
    }
    //public async void AddGroup(Group group)
    //{
    //    await _groupRepository.InsertOneAsync(group);
    //}

    //public async void ReplaceGroup(Group group)
    //{
    //    try
    //    {
    //        await _groupRepository.ReplaceOneAsync(group);

    //    }
    //    catch(Exception ex)
    //    {
    //        _logger.LogInformation("Message Services", ex);
    //    }
    //}

    //public async void AddConnection(Connection connection)
    //{
    //    await _connectionRepository.InsertOneAsync(connection);
    //}
    //public async Task<Connection> GetConnection(string connectionId)
    //{
    //    return await _connectionRepository.FindByIdAsync(connectionId);
    //}

    //public async Task<Group> GetGroupForConnection(string connectionId)
    //{
    //    return await _groupRepository.FindGroupForConnection(connectionId);
    //}

    //public async Task<Group> GetMessageGroup(string groupName)
    //{
    //    return await _groupRepository.FindMessageGroup(groupName);
    //}



    //public async void RemoveConnection(Connection connection)
    //{
    //    await _connectionRepository.DeleteByIdAsync(connection.Id);
    //}




}
