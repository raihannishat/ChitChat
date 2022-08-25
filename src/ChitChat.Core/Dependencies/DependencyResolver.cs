using ChitChat.Core.RabbitMQ;
using ChitChat.Core.RabbitMQ.Models;
using ChitChat.Core.Repositories;
using ChitChat.Core.Services;
using ChitChat.Data.Configurations;
using ChitChat.Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ChitChat.Core.Dependencies;

public static class DependencyResolver
{
	public static void CoreResolver(this IServiceCollection services)
	{
		services.AddScoped<IConnectionRepository, ConnectionRepository>();
		services.AddScoped<IGroupRepository, GroupRepository>();
		services.AddSingleton<IMessageRepository, MessageRepository>();
		services.AddSingleton<IMessageService, MessageService>();
		services.AddSingleton<ISignalRConsumer, SignalRConsumer>();
		services.AddSingleton<IDBConsumer, DBConsumer>();
		services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();
		services.AddSingleton<ExchangerQueueSetting>();
	}
}
