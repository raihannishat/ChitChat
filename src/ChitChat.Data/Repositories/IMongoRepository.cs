namespace ChitChat.Data.Repositories;

public interface IMongoRepository<TDocument> where TDocument : IDocument
{
    Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

    Task<TDocument> FindByIdAsync(string id);

    Task InsertOneAsync(TDocument document);

    Task ReplaceOneAsync(TDocument document);

    Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);

    Task DeleteByIdAsync(string id);

    Task<List<TDocument>> GetAll();
}
