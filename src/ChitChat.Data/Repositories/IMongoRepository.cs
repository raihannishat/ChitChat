using ChitChat.Data.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Data.Repositories
{
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
}
