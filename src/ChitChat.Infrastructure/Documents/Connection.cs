namespace ChitChat.Infrastructure.Documents;

[BsonCollection("Connections")]

public class Connection : Document
{
    public Connection()
    {

    }

    public Connection(string connectionId, string username)
    {
        ConnectionId = connectionId;
        Username = username;
    }

    public string ConnectionId { get; set; }
    public string Username { get; set; }
}
