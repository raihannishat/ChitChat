using ChitChat.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace ChitChat.Infrastructure;

public static class ConfigureServices
{
	public static IServiceCollection AddInfractructureServices(this IServiceCollection services,
		IConfiguration configuration)
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

		services.AddSingleton<IRabbitMQSettings>(configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>());


		return services;
	}
}
