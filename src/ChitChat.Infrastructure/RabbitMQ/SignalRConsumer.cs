namespace ChitChat.Infrastructure.RabbitMQ;

public class SignalRConsumer : ISignalRConsumer
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ExchangerQueueSetting _exchangerQueueSetting;
    private readonly ConnectionFactory _factory;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<SignalRConsumer> _logger;

    public SignalRConsumer(IServiceProvider serviceProvider, ExchangerQueueSetting exchangerQueueSetting,
        IRabbitMQSettings rabbitMQSettings, ILogger<SignalRConsumer> logger)
    {
        _serviceProvider = serviceProvider;
        _exchangerQueueSetting = exchangerQueueSetting;
        _factory = new ConnectionFactory() { Uri = new Uri(rabbitMQSettings.URI) };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _logger = logger;
    }

    public virtual void Connect()
    {
        var signalRQueue = _exchangerQueueSetting.SigalRQueue;

        _channel.QueueDeclare(signalRQueue, true, false, false, null);
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            var chatHub = (IHubContext<MessageHub>)_serviceProvider.GetService(typeof(IHubContext<MessageHub>))!;

            var body = ea.Body.ToArray();
            var data = Encoding.UTF8.GetString(body);
            var message = JsonSerializer.Deserialize<Message>(data)!;

            _logger.LogInformation(signalRQueue + "got message --->" + message);

            var groupName = GetGroupName(message.SenderUsername, message.RecipientUsername);

            await chatHub.Clients.Group(groupName).SendAsync("NewMessage", message);
        };

        _channel.BasicConsume(signalRQueue, true, consumer);
    }

    private string GetGroupName(string caller, string other)
    {
        var stringCompare = string.CompareOrdinal(caller, other) < 0;
        return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
    }
}
