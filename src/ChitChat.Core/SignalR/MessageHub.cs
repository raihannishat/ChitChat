using AutoMapper;
using ChitChat.Core.Documents;
using Microsoft.AspNetCore.SignalR;
using ChitChat.Core.Services;
using ChitChat.Core.Extentions;
using ChitChat.Core.BusinessObjects;
using ChitChat.Identity.Services;
using ChitChat.Core.Repositories;
using System.Security.Claims;

namespace ChitChat.Core.SignalR;

public class MessageHub : Hub
{
	private readonly IMapper _mapper;
	private readonly IMessageService _messageService;
	private readonly IUserService _userService;
	

	public MessageHub(IMapper mapper, IMessageService messageService, 
		IUserService userService)
	{
		_messageService = messageService;
		_mapper = mapper;
		_userService = userService;
	}

	public override async Task OnConnectedAsync()
	{	
		var httpContext = Context.GetHttpContext();
		var sender = httpContext.Request.Query["sender"].ToString();
		var receiver = httpContext.Request.Query["receiver"].ToString();
		var groupName = GetGroupName(sender, receiver);
		await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
		var group = await AddToGroup(groupName, sender);
		_messageService.ReplaceGroup(group);
		await Clients.Group(groupName).SendAsync("UpdatedGroup", group);

		var messages = await _messageService.
			GetMessageThread(sender, receiver);

		await Clients.Caller.SendAsync("ReceiveMessageThread", messages);
	}

	public override async Task OnDisconnectedAsync(Exception exception)
	{
		var group = await RemoveFromMessageGroup();

		await Clients.Group(group.Name).SendAsync("UpdatedGroup", group);
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
			MessageSent = DateTime.Now,
			Sender = sender,
			Recipient = recipient,
			SenderName = sender.Name,
			RecipientName = recipient.Name,
			Content = createMessageBusinessObject.Content
		};

		var groupName = GetGroupName(sender.Name, recipient.Name);

		//getting group name for finding the receiver connection id to check message already read or not
		//var group = await _messageService.GetMessageGroup(groupName);
  //      if (group.Connections.Any(x => x.Username == recipient.Name))
  //      {
		//	message.DateRead = DateTime.Now;

		//}

		_messageService.AddMessage(message);

		await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageBusinessObject>(message));
		
	}

	private async Task<Group> AddToGroup(string groupName, string sender)
	{
		var group = await _messageService.GetMessageGroup(groupName);
		var connection = new Connection(Context.ConnectionId, sender);

		if (group == null)
		{
			group = new Group(groupName);
			_messageService.AddGroup(group);
		}

		// group.Connections.Add(connection);

		group.Connections.Add(_mapper.Map<Connection>(connection));

		//_messageService.ReplaceGroup(group);
		_messageService.AddConnection(connection);

		return group;

		//use try catch instead
		//throw new HubException("Failed to join group");
	}

	private async Task<Group> RemoveFromMessageGroup()
	{
		var group = await _messageService.GetGroupForConnection(Context.ConnectionId);
		var connection = group.Connections.FirstOrDefault(x => 
									x.ConnectionId == Context.ConnectionId);

		group.Connections.Remove(connection);
		_messageService.RemoveConnection(connection);

		_messageService.ReplaceGroup(group);

		return group;

		//use try catch instead
		//throw new HubException("Failed to remove from group");
	}

	private string GetGroupName(string caller, string other)
	{
		var stringCompare = string.CompareOrdinal(caller, other) < 0;
		return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
	}
}
