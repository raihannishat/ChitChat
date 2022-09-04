using Infrastructure.DbContext;
using MongoDB.Driver;

namespace Infrastructure
{
    public class LearnathonDbContext : ILearnathonDbContext
    {
        private IMongoDatabase _database { get; set; }
        public MongoClient _mongoClient { get; set; }
        public LearnathonDbContext(IMongoDbSetting setting)
        {
            //var database = new MongoClient(setting.ConnectionString).GetDatabase(setting.DatabaseName);
            //_collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }
        public IMongoCollection<T> GetCollection<T>(string name = "")
        {
            throw new NotImplementedException();
        }
    }
}
