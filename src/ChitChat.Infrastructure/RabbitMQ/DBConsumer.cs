namespace ChitChat.Infrastructure.RabbitMQ;

public class DBConsumer : IDBConsumer
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMessageService _messageService;
    private readonly IMapper _mapper;
    private readonly ILogger<DBConsumer> _logger;
    private readonly ExchangerQueueSetting _exchangerQueueSetting;
    private readonly ConnectionFactory _factory;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private static readonly string _url = "amqps://shmhruyd:Vc6vZ4jqCuzvo_XqFUwvhT0xQDLxKHsm@armadillo.rmq.cloudamqp.com/shmhruyd";



    public DBConsumer(IServiceProvider serviceProvider, ExchangerQueueSetting exchangerQueueSetting,
        IMessageService messageService, IMapper mapper, ILogger<DBConsumer> logger)
    {
        _serviceProvider = serviceProvider;
        _messageService = messageService;
        _mapper = mapper;
        _logger = logger;
        _exchangerQueueSetting = exchangerQueueSetting;
        _factory = new ConnectionFactory() { Uri = new Uri(_url) };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public virtual void Connect()
    {
        var databaseQueue = _exchangerQueueSetting.DatabaseQueue;

        _channel.QueueDeclare(databaseQueue, true, false, false, null);
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var data = Encoding.UTF8.GetString(body);

            try
            {
                var message = JsonSerializer.Deserialize<Message>(data);

                Console.WriteLine(databaseQueue + "got message --->" + message);

                await _messageService.AddMessage(_mapper.Map<DTOs.MessageDTO>(message));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("DbConsumer : " + ex);
            }
        };

        _channel.BasicConsume(databaseQueue, true, consumer);
    }
}
