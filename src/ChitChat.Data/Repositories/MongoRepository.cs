namespace ChitChat.Data.Repositories;

public class MongoRepository<TDocument> : IRepository<TDocument>
        where TDocument : IDocument
{
    protected readonly IMongoCollection<TDocument> _collection;

    public MongoRepository(IMongoDbSettings settings)
    {
        var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
    }
    public Task<TDocument> FindByIdAsync(string id)
    {

        return Task.Run(() =>
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            return _collection.Find(filter).SingleOrDefaultAsync();
        });
    }
    public Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
    }
    public Task<List<TDocument>> GetAll()
    {
        return Task.Run(() => _collection.Find(_ => true).ToListAsync());
    }
    public Task InsertOneAsync(TDocument document)
    {
        return Task.Run(() => _collection.InsertOneAsync(document));
    }

    public Task ReplaceOneAsync(TDocument document)
    {
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
        return _collection.FindOneAndReplaceAsync(filter, document);
    }
    public Task DeleteByIdAsync(string id)
    {
        return Task.Run(() =>
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            _collection.FindOneAndDeleteAsync(filter);
        });
    }
    public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
    }
    private protected string GetCollectionName(Type documentType)
    {
        return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                typeof(BsonCollectionAttribute), true).FirstOrDefault()).CollectionName;
    }
}
