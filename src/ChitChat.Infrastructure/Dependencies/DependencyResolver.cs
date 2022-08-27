namespace ChitChat.Infrastructure.Dependencies;

public static class DependencyResolver
{
	public static void InfrastructureResolver(this IServiceCollection services)
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
