namespace ChitChat.Infrastructure.RabbitMQ;

public class RabbitMQPublisher : IRabbitMQPublisher
{
    private readonly ConnectionFactory _factory;
    private readonly ExchangerQueueSetting _exchangerQueueSetting;

    public RabbitMQPublisher(ExchangerQueueSetting exchangerQueueSetting)
    {
        _exchangerQueueSetting = exchangerQueueSetting;
        _factory = new ConnectionFactory() { HostName = "localhost" };
    }

    public Task SendMessageToQueue(Message message)
    {
        string databaseQueue = _exchangerQueueSetting.DatabaseQueue;
        string signalRQueue = _exchangerQueueSetting.SigalRQueue;
        string exchange = _exchangerQueueSetting.Exchange;

        using (var connection = _factory.CreateConnection())

        using (IModel channel = connection.CreateModel())
        {
            channel.ExchangeDeclare(exchange, ExchangeType.Fanout);

            channel.QueueDeclare(databaseQueue, true, false, false, null);
            channel.QueueDeclare(signalRQueue, true, false, false, null);

            channel.QueueBind(databaseQueue, exchange, "");
            channel.QueueBind(signalRQueue, exchange, "");

            var publisherMessages = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(publisherMessages);

            channel.BasicPublish(exchange: exchange, routingKey: "", basicProperties: null, body: body);

            Console.WriteLine(" [x] Sent {0}", message);
        }

        return Task.CompletedTask;
    }
}
