namespace ChitChat.Infrastructure.RabbitMQ.Models;

public class ExchangerQueueSetting
{
    public string DatabaseQueue { get { return "database"; } }
    public string SigalRQueue { get { return "signalR1"; } }
    public string Exchange { get { return "DIU_Raizor"; } }
}
