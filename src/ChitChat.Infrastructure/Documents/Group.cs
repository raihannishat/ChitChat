namespace ChitChat.Infrastructure.Documents;

[BsonCollection("Groups")]
public class Group : Document
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Group()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Group(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    public ICollection<Connection> Connections { get; set; } = new List<Connection>();
}
