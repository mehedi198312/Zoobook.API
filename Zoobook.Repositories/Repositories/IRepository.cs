using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Zoobook.Repositories
{
    public interface IRepository<T> where T : class
    {


        #region "Get"

        Task<T> Get(int id);

        Task<T> Get(string id);

        Task<List<T>> GetList();

        Task<List<T>> GetList(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");

        Task<T> FirstOrDefaultAsync();

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "");

        Task<T> LastOrDefaultAsync();

        Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> criteria);

        Task<string> MaxAsync(Expression<Func<T, string>> maxBy, Expression<Func<T, bool>> criteria);

        Task<string> NextAsync(Expression<Func<T, string>> itemName, int itemLength, Expression<Func<T, bool>> criteria);

        Task<int> CountAsync(Expression<Func<T, bool>> criteria);

        #endregion

        #region "Insert"

        Task<T> Insert(T obj);

        Task<IList<T>> InsertListAsync(IList<T> entityList);

        #endregion

        #region "Update"

        Task Update(T obj);

        Task UpdateAsync(T entity, Expression<Func<T, object>>[] properties);

        Task UpdateExceptAsync(T entity, Expression<Func<T, object>>[] properties);

        Task UpdateExceptOneAsync(T entity, Expression<Func<T, object>>[] properties);

        Task UpdateListAsync(IList<T> entityList);

        #endregion

        #region "Permanent Delete"

        Task PermanentDelete(T obj);

        Task PermanentDeleteById(int id);

        Task PermanentDeleteById(string id);

        Task PermanentDeleteRangeAsync(IList<T> entity);

        #endregion
        
        Task<bool> Any(Expression<Func<T, bool>> filter);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        
    }
}