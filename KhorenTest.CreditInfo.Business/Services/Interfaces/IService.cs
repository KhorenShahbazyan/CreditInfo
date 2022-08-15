using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace KhorenTest.CreditInfo.Business.Services.Interfaces
{
    public interface IService<T, T1>
           where T : class
           where T1 : class
    {
        #region T
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<IReadOnlyCollection<T>> GetAllEntityAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        #endregion


        Task<T1> AddAsync(T1 entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<T1> AddWithSaveChangesAsync(T1 entity, CancellationToken cancellationToken = default(CancellationToken));
        Task AddRangeAsync(IEnumerable<T1> entities, CancellationToken cancellationToken = default(CancellationToken));
        Task AddIfNotExistsAsync(T1 entity, Expression<Func<T1, bool>> predicate = null, CancellationToken cancellationToken = default(CancellationToken));
        Task AddRangeIfNotExistsAsync(IEnumerable<T1> entities, Func<T, object> predicate, CancellationToken cancellationToken = default(CancellationToken));

        void Update(T1 entity);
        void UpdateRange(IEnumerable<T1> entities);
        void Remove(T1 entity);
        void RemoveByRange(IEnumerable<T1> entities);

        Task<IReadOnlyCollection<T1>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<T1>> FindByAsync(Expression<Func<T1, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<T1>> FindByAsync(Expression<Func<T1, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken), params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> FindEntitiesByAsync(Expression<Func<T, bool>> predicate, bool withTracking = false, CancellationToken cancellationToken = default(CancellationToken), params Expression<Func<T, object>>[] includeProperties);


        Task<bool> AnyAsync(Expression<Func<T1, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> CountAsync(Expression<Func<T1, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        //Task<IEnumerable<T1>> GetAsync(SearchModel search = null, Expression<Func<T, object>>[] includes = null, CancellationToken cancellationToken = default(CancellationToken));
        //Task<SearchResultModel<T1>> GetWithCountAsync(SearchModel search = null, Expression<Func<T, object>>[] includes = null, CancellationToken cancellationToken = default(CancellationToken));

        //Task<IEnumerable<T1>> GetAsync(Expression<Func<T1, bool>> filter = null, Expression<Func<T, object>>[] includes = null, SearchModel search = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<T1>> GetAsync(Expression<Func<T1, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Expression<Func<T, object>>[] includes = null, int? startIndex = null, int? count = null, CancellationToken cancellationToken = default(CancellationToken));


    }
}
