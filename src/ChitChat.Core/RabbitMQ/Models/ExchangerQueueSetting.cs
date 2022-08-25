using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Core.RabbitMQ.Models;
public class ExchangerQueueSetting
{
    public string DatabaseQueue { get { return "database"; } }
    public string SigalRQueue { get { return "signalR"; } }
    public string Exchange { get { return "DIU_Raizor"; } }
}
