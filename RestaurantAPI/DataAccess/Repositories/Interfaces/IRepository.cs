using System.Collections.Generic;

namespace DataAccess
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetCountWithOffsetByOrdering(int count, int offset, string ordering);

        TEntity GetById(int id);

        // if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.Serializable); for all transaction purposes
        TEntity Create(TEntity obj, bool transactionEndpoint = true);

        TEntity Update(TEntity obj, bool transactionEndpoint = true);

        bool Delete(TEntity obj, bool transactionEndpoint = true);
    }
}