using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zoobook.Data;

namespace Zoobook.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {

        private readonly EmployeeRecordsDbContext _appsDbContext;
        
        protected Repository(EmployeeRecordsDbContext context)
        {
            _appsDbContext = context;
        }
        
        #region "Get"

        public virtual async Task<T> Get(int id)
        {
            return await _appsDbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> Get(string id)
        {
            return await _appsDbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<List<T>> GetList()
        {
            return await _appsDbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<List<T>> GetList(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _appsDbContext.Set<T>();

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public virtual async Task<T> FirstOrDefaultAsync()
        {
            var result = await _appsDbContext.Set<T>().AsNoTracking().ToListAsync();
            return result.FirstOrDefault();
        }

        //public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> criteria, string includeProperties = "")
        //{
        //    var result = await _appsDbContext.Set<T>().AsNoTracking().Where(criteria).ToListAsync();
        //    return result.FirstOrDefault();
        //}

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = _appsDbContext.Set<T>();

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);
            
            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<T> LastOrDefaultAsync()
        {
            var result = await _appsDbContext.Set<T>().AsNoTracking().ToListAsync();
            return result.LastOrDefault();
        }

        public virtual async Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> criteria)
        {
            var result = await _appsDbContext.Set<T>().AsNoTracking().Where(criteria).ToListAsync();
            return result.LastOrDefault();
        }

        public virtual async Task<string> MaxAsync(Expression<Func<T, string>> maxBy, Expression<Func<T, bool>> criteria)
        {
            return await _appsDbContext.Set<T>().Where(criteria).MaxAsync(maxBy);
        }

        public virtual async Task<string> NextAsync(Expression<Func<T, string>> itemName, int itemLength, Expression<Func<T, bool>> criteria)
        {
            var maxValue = await _appsDbContext.Set<T>().Where(criteria).MaxAsync(itemName);
            int nextValue = 0;

            if (!string.IsNullOrWhiteSpace(maxValue))
            {
                nextValue = Convert.ToInt32(maxValue) + 1;
            }

            string nexVal = Convert.ToString(nextValue);
            return nexVal.Length > itemLength ? string.Empty : nexVal.PadLeft(itemLength, '0');
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _appsDbContext.Set<T>().Where(criteria).CountAsync();
        }

        #endregion

        #region "Insert"

        public async Task<T> Insert(T obj)
        {
            _appsDbContext.Set<T>().Add(obj);
            await _appsDbContext.SaveChangesAsync();
            return obj;
        }

        public async Task<IList<T>> InsertListAsync(IList<T> entityList)
        {
            _appsDbContext.Set<T>().AddRange(entityList);
            await _appsDbContext.SaveChangesAsync();
            return entityList;
        }

        #endregion

        #region "Update"

        public virtual async Task Update(T obj)
        {
            _appsDbContext.Entry(obj).State = EntityState.Modified;
            await _appsDbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity, Expression<Func<T, object>>[] properties)
        {
            _appsDbContext.Set<T>().Attach(entity);

            foreach (var property in properties)
            {
                _appsDbContext.Entry(entity).Property(property).IsModified = true;
            }
            await _appsDbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateExceptAsync(T entity, Expression<Func<T, object>>[] properties)
        {
            _appsDbContext.Set<T>().Attach(entity);

            foreach (var property in properties)
            {
                _appsDbContext.Entry(entity).Property(property).IsModified = false;
            }
            await _appsDbContext.SaveChangesAsync();
        }
        
        public virtual async Task UpdateExceptOneAsync(T entity, Expression<Func<T, object>>[] properties)
        {
            _appsDbContext.Set<T>().Attach(entity);

            foreach (var property in properties)
            {
                _appsDbContext.Entry(entity).State = EntityState.Modified;
                _appsDbContext.Entry(entity).Property(property).IsModified = false;
            }
            await _appsDbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateListAsync(IList<T> entityList)
        {
            foreach (var entity in entityList)
            {
                _appsDbContext.Entry(entity).State = EntityState.Modified;
            }
            await _appsDbContext.SaveChangesAsync();
        }

        #endregion
        
        #region "Permanent Delete"

        public virtual async Task PermanentDelete(T obj)
        {
            _appsDbContext.Set<T>().Remove(obj);
            await _appsDbContext.SaveChangesAsync();
        }

        public virtual async Task PermanentDeleteById(int id)
        {
            var obj = await Get(id);
            await PermanentDelete(obj);
        }

        public virtual async Task PermanentDeleteById(string id)
        {
            var obj = await Get(id);
            await PermanentDelete(obj);
        }

        public async Task PermanentDeleteRangeAsync(IList<T> entity)
        {
            _appsDbContext.Set<T>().RemoveRange(entity);
            await _appsDbContext.SaveChangesAsync();
        }

        #endregion

        public virtual async Task<bool> Any(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _appsDbContext.Set<T>();
            return await query.AnyAsync(filter);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _appsDbContext.Set<T>().Where(predicate);
        }

    }
}