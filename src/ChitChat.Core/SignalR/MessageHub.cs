using AutoMapper;
using ChitChat.Core.Documents;
using Microsoft.AspNetCore.SignalR;
using ChitChat.Core.Services;
using ChitChat.Core.Extentions;
using ChitChat.Core.BusinessObjects;
using ChitChat.Identity.Services;
using ChitChat.Core.Repositories;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using ChitChat.Core.RabbitMQ;

namespace ChitChat.Core.SignalR;

public class MessageHub : Hub
{
	private readonly ILogger<MessageHub> _logger;
	private readonly IMapper _mapper;
	private readonly IMessageService _messageService;
	private readonly IUserService _userService;
	private readonly IRabbitMQPublisher _rabbitMQpublisher;
	

	public MessageHub(IMapper mapper, IMessageService messageService, 
		IUserService userService, ILogger<MessageHub> logger, IRabbitMQPublisher rabbitMQPublisher)
	{
		_messageService = messageService;
		_mapper = mapper;
		_userService = userService;
		_rabbitMQpublisher = rabbitMQPublisher;
		_logger = logger;
	}

	public override async Task OnConnectedAsync()
	{	
		var httpContext = Context.GetHttpContext();
		var sender = httpContext.Request.Query["sender"].ToString();
		var receiver = httpContext.Request.Query["receiver"].ToString();
		var groupName = GetGroupName(sender, receiver);
		await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
		//var group = await AddToGroup(groupName, sender);
		//await Clients.Group(groupName).SendAsync("UpdatedGroup", group);

		var messages = await _messageService.
			GetMessageThread(sender, receiver);

		await Clients.Caller.SendAsync("ReceiveMessageThread", messages);
	}

	public override async Task OnDisconnectedAsync(Exception exception)
	{
		//var group = await RemoveFromMessageGroup();

		//await Clients.Group(group.Name).SendAsync("UpdatedGroup", group);
		await base.OnDisconnectedAsync(exception);
	}

	public async Task SendMessage(CreateMessageBusinessObject createMessageBusinessObject)
	{
		var username = createMessageBusinessObject.Sender;
		
		if (username == createMessageBusinessObject.Receiver.ToLower())
			throw new HubException("You cannot send messages to yourself");

		var sender = await _userService.GetUserByNameAsync(username);
		var recipient = await _userService.GetUserByNameAsync(createMessageBusinessObject.Receiver);

		if (recipient == null) throw new HubException("Not found user");

		var message = new MessageBusinessObject
		{
			SenderId = sender.Id,
			RecipientId = recipient.Id,
			SenderUsername = sender.Name,
			RecipientUsername = recipient.Name,
			Content = createMessageBusinessObject.Content,
			MessageSent = DateTime.UtcNow
		};

		await _rabbitMQpublisher.SendMessageToQueue(_mapper.Map<Message>(message));

		//var groupName = GetGroupName(sender.Name, recipient.Name);

		//await _messageService.AddMessage(message);

		//await Clients.Group(groupName).SendAsync("NewMessage",message);
		
	}

	//private async Task<Group> AddToGroup(string groupName, string sender)
	//{
	//	var group = await _messageService.GetMessageGroup(groupName);
	//	var connection = new Connection(Context.ConnectionId, sender);

	//	if (group == null)
	//	{
	//		group = new Group(groupName);
	//		group.Connections.Add(_mapper.Map<Connection>(connection));
	//		_messageService.AddGroup(group);
	//	}
		//else
		//{
		//	group.Connections.Add(connection);
		//	var updatedGroup = new Group
		//	{
		//		Id = group.Id,
		//		Name = group.Name,
		//		Connections = group.Connections
		//          };
		//	_messageService.ReplaceGroup(updatedGroup);
		//}
	//	group.Connections.Add(_mapper.Map<Connection>(connection));
	//	_messageService.AddConnection(connection);

	//	return group;

	//	//use try catch instead
	//	//throw new HubException("Failed to join group");
	//}

	//private async Task<Group> RemoveFromMessageGroup()
	//{
	//	var group = await _messageService.GetGroupForConnection(Context.ConnectionId);
	//	var connection = group.Connections.FirstOrDefault(x => 
	//								x.ConnectionId == Context.ConnectionId);

	//	group.Connections.Remove(connection);
	//	_messageService.RemoveConnection(connection);

	//	var updatedGroup = new Group
	//	{
	//		Id = group.Id,
	//		Name = group.Name,
	//		Connections = group.Connections
	//	};
	//	_messageService.ReplaceGroup(updatedGroup);
	//	return group;

		//use try catch instead
		//throw new HubException("Failed to remove from group");
	//}

	private string GetGroupName(string caller, string other)
	{
		var stringCompare = string.CompareOrdinal(caller, other) < 0;
		return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
	}
}
