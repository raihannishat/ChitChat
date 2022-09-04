using MongoDB.Driver;

namespace Infrastructure.DbContext
{
    public interface ILearnathonDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name = "");
    }
}
