using ChitChat.Data.Configurations;
using ChitChat.Data.Documents;

namespace ChitChat.Core.Documents;

[BsonCollection("Groups")]
public class Group : Document
{
    public Group()
    {
    }

    public Group(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    public ICollection<Connection> Connections { get; set; } = new List<Connection>();
}
