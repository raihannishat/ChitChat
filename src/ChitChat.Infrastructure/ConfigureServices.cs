namespace ChitChat.Infrastructure;

public static class ConfigureServices
{
	public static IServiceCollection AddInfractructureServices(this IServiceCollection services)
	{
		services.AddScoped<IConnectionRepository, ConnectionRepository>();
		services.AddScoped<IGroupRepository, GroupRepository>();
		services.AddSingleton<IMessageRepository, MessageRepository>();
		services.AddSingleton<IMessageService, MessageService>();
		services.AddSingleton<ISignalRConsumer, SignalRConsumer>();
		services.AddSingleton<IDBConsumer, DBConsumer>();
		services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();
		services.AddSingleton<ExchangerQueueSetting>();

		services.AddSignalR(hubOptions =>
		{
			hubOptions.EnableDetailedErrors = true;
			hubOptions.MaximumReceiveMessageSize = 102400000;
		});

		return services;
	}
}
