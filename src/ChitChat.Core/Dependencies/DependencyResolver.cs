using ChitChat.Infrastructure.RabbitMQ;
using ChitChat.Infrastructure.RabbitMQ.Models;
using ChitChat.Infrastructure.Repositories;
using ChitChat.Infrastructure.Services;
using ChitChat.Data.Configurations;
using ChitChat.Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ChitChat.Infrastructure.Dependencies;

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
