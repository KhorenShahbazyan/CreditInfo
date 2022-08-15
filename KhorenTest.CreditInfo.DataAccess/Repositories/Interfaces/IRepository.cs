using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KhorenTest.CreditInfo.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> AllIncluding(params string[] includeProperties);
        IQueryable<T> Entity { get; }

        void Update(T entity);
        //void Update(long id);

        void UpdateRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveByRange(IEnumerable<T> entities);

        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));

        Task AddIfNotExistsAsync(T entity, Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default(CancellationToken));
        Task AddRangeIfNotExistsAsync(IEnumerable<T> entities, Func<T, object> predicate, CancellationToken cancellationToken = default(CancellationToken));

        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        Task<ICollection<T>> FindByWithTrackingAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        Task<ICollection<T>> FindByWithTrackingAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken), params Expression<Func<T, object>>[] includes);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includeProperties, CancellationToken cancellationToken = default(CancellationToken));
        Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));


        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Expression<Func<T, object>>[] includes = null, int? startIndex = null, int? count = null, CancellationToken cancellationToken = default(CancellationToken));
        
    }
}
