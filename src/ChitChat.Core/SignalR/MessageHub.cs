using AutoMapper;
using ChitChat.Core.Documents;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChitChat.Core.Services;
using ChitChat.Core.Extentions;
using ChitChat.Core.BusinessObjects;
using ChitChat.Identity.Services;
using ChitChat.Core.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace ChitChat.Core.SignalR;

public class MessageHub : Hub
{
	private readonly IMapper _mapper;
	private readonly IMessageService _messageService;
	private readonly IUserService _userService;
	private readonly IGroupRepository _groupRepository;
	private readonly IConnectionRepository _connectionRepository;

	public MessageHub(IMapper mapper, IMessageService messageService, 
		IUserService userService, IGroupRepository groupRepository,
		IConnectionRepository connectionRepository)
	{
		_messageService = messageService;
		_mapper = mapper;
		_userService = userService;
		_groupRepository = groupRepository;	
		_connectionRepository = connectionRepository;
	}

	public override async Task OnConnectedAsync()
	{
		var httpContext = Context.GetHttpContext();
		var otherUser = httpContext.Request.Query["user"].ToString();
		var groupName = GetGroupName(Context.User.GetUsername(), otherUser);
		await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
		var group = await AddToGroup(groupName);
		await Clients.Group(groupName).SendAsync("UpdatedGroup", group);

		var messages = await _messageService.
			GetMessageThread(Context.User.GetUsername(), otherUser);

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
		var username = Context.User.GetUsername();

		if (username == createMessageBusinessObject.RecipientUsername.ToLower())
			throw new HubException("You cannot send messages to yourself");

		var sender = await _userService.GetUserByNameAsync(username);
		var recipient = await _userService.GetUserByNameAsync(createMessageBusinessObject.RecipientUsername);

		if (recipient == null) throw new HubException("Not found user");

		var message = new MessageBusinessObject
		{
			Sender = sender,
			Recipient = recipient,
			SenderName = sender.Name,
			RecipientName = recipient.Name,
			Content = createMessageBusinessObject.Content
		};

		var groupName = GetGroupName(sender.Name, recipient.Name);

		var group = await _messageService.GetMessageGroup(groupName);

		_messageService.AddMessage(message);

		await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageBusinessObject>(message));
		
	}

	private async Task<Group> AddToGroup(string groupName)
	{
		var group = await _messageService.GetMessageGroup(groupName);
		var connection = new Connection(Context.ConnectionId, Context.User.GetUsername());

		if (group == null)
		{
			group = new Group(groupName);
			_messageService.AddGroup(group);
		}

		// group.Connections.Add(connection);

		group.Connections.Add(_mapper.Map<Connection>(connection));

		await _connectionRepository.InsertOneAsync(connection);

		return group;

		throw new HubException("Failed to join group");
	}

	private async Task<Group> RemoveFromMessageGroup()
	{
		var group = await _messageService.GetGroupForConnection(Context.ConnectionId);
		var connection = group.Connections.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
		_messageService.RemoveConnection(connection);
		group.Connections.Remove(connection);
		
		return group;

		throw new HubException("Failed to remove from group");
	}

	private string GetGroupName(string caller, string other)
	{
		var stringCompare = string.CompareOrdinal(caller, other) < 0;
		return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
	}
}
