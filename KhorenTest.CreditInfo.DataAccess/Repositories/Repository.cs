using KhorenTest.CreditInfo.DataAccess.Extensions;
using KhorenTest.CreditInfo.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace KhorenTest.CreditInfo.DataAccess.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<T> DbSet;
        protected readonly ILogger Logger;

        protected Repository(DbContext context, ILogger logger)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            DbSet = DbContext.Set<T>();
            Logger.LogInformation($"{nameof(T)}");
        }

        /// <summary>
        /// //     Specifies related entities to include in the query results. The navigation property
        //     to be included is specified starting with the type of entity being queried (TEntity).
        //     If you wish to include additional types based on the navigation properties of
        //  the type being included, then chain a call to Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ThenInclude``3(Microsoft.EntityFrameworkCore.Query.IIncludableQueryable{``0,System.Collections.Generic.IEnumerable{``1}},System.Linq.Expressions.Expression{System.Func{``1,``2}})
        //     after this call.
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual IQueryable<T> AllIncluding(params string[] includeProperties)
        {
            IQueryable<T> query = DbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<T> Entity => DbSet;

        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                Logger.LogCritical($"{nameof(AddAsync)} - {nameof(entity)} is null");
                throw new ArgumentNullException(nameof(entity));
            }

            Logger.LogInformation($"{nameof(AddAsync)} - Begin creation of an {nameof(entity)}", entity.ToString());
            return (await DbSet.AddAsync(entity, cancellationToken)).Entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entities == null)
            {
                Logger.LogCritical($"{nameof(AddRangeAsync)} - The collection {nameof(entities)} is null");
                throw new ArgumentNullException(nameof(entities));
            }

            await DbSet.AddRangeAsync(entities, cancellationToken);
        }

        public virtual async Task AddIfNotExistsAsync(T entity, Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var exists = DbSet.Take(5000).Any(predicate ?? throw new ArgumentNullException(nameof(predicate)));

            if (!exists)
            {
                await DbSet.AddAsync(entity, cancellationToken);
            }
        }

        public virtual async Task AddRangeIfNotExistsAsync(IEnumerable<T> entities, Func<T, object> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entitiesExist = from ent in DbSet
                                where entities.Any(add => predicate(ent).Equals(predicate(add)))
                                select ent;

            await DbSet.AddRangeAsync(entities.Except(entitiesExist), cancellationToken);
        }

        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            ICollection<T> result = null;

            try
            {
                Logger.LogInformation($"{nameof(FindByAsync)} - Find by {nameof(predicate)} {predicate?.Body}");

                return result = await DbSet
                    .AsNoTracking()
                    .Where(predicate ?? throw new ArgumentNullException(nameof(predicate)))
                    .ToListAsync(cancellationToken);
            }
            finally
            {
                Logger.LogInformation($"Result of {nameof(FindByAsync)}", result);
            }
        }

        public virtual async Task<ICollection<T>> FindByWithTrackingAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            ICollection<T> result = null;

            try
            {
                Logger.LogInformation($"{nameof(FindByWithTrackingAsync)} - Find by {nameof(predicate)} {predicate?.Body}");

                return result = await DbSet
                    .Where(predicate ?? throw new ArgumentNullException(nameof(predicate)))
                    .ToListAsync(cancellationToken);
            }
            finally
            {
                Logger.LogInformation($"Result of {nameof(FindByAsync)}", result);
            }
        }

        public virtual async Task<ICollection<T>> FindByWithTrackingAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken), params Expression<Func<T, object>>[] includes)
        {
            ICollection<T> result = null;

            try
            {
                Logger.LogInformation($"{nameof(FindByWithTrackingAsync)} - Find by {nameof(predicate)} {predicate?.Body}");

                return result = await DbSet
                    .IncludeMany(includes)
                    .Where(predicate ?? throw new ArgumentNullException(nameof(predicate)))
                    .ToListAsync(cancellationToken);
            }
            finally
            {
                Logger.LogInformation($"Result of {nameof(FindByAsync)}", result);
            }
        }

        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includeProperties, CancellationToken cancellationToken = default(CancellationToken))
        {
            ICollection<T> result = null;

            try
            {
                Logger.LogInformation($"{nameof(FindByAsync)} - Find by {nameof(predicate)} {predicate?.Body}");

                return result = await AllIncluding(includeProperties)
                                     .AsNoTracking()
                                     .Where(predicate ?? throw new ArgumentNullException(nameof(predicate)))
                                     .ToListAsync(cancellationToken);
            }
            finally
            {
                Logger.LogInformation($"Result of {nameof(FindByAsync)}", result);
            }
        }

        public virtual async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ICollection<T> result = null;

            try
            {
                Logger.LogInformation($"{nameof(GetAllAsync)}");
                return result = await DbSet
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
            }
            finally
            {
                Logger.LogInformation($"Result of {nameof(GetAllAsync)}", result);
            }
        }

        //public virtual void Update(Brand brand)
        //{
        //    if (brand == null)
        //    {
        //        Logger.LogCritical($"{nameof(Update)} - The {nameof(brand)} is null");
        //        throw new ArgumentNullException(nameof(brand));
        //    }

        //    Logger.LogInformation($"{nameof(Update)} - Begin update of an {nameof(brand)}", brand.ToString());
        //    DbSet.Single(x=> x.Id==brand.Id);
        //}

        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                Logger.LogCritical($"{nameof(Update)} - The {nameof(entity)} is null");
                throw new ArgumentNullException(nameof(entity));
            }

            Logger.LogInformation($"{nameof(Update)} - Begin update of an {nameof(entity)}", entity.ToString());

            DbSet.Update(entity);


        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                Logger.LogCritical($"{nameof(UpdateRange)} - The {nameof(entities)} collection is null");
                throw new ArgumentNullException(nameof(entities));
            }

            Logger.LogInformation($"{nameof(UpdateRange)} - Begin update range of an {nameof(entities)}", entities.ToString());
            var updateRange = entities as T[] ?? entities.ToArray();
            DbSet.UpdateRange(updateRange);
        }

        public virtual void Remove(T entity)
        {
            if (entity == null)
            {
                Logger.LogCritical($"{nameof(Remove)} - The {nameof(entity)} is null");
                throw new ArgumentNullException(nameof(entity));
            }

            Logger.LogInformation($"{nameof(Remove)} - Begin remove of an {nameof(entity)}", entity.ToString());
            DbSet.Remove(entity);
        }

        public virtual void RemoveByRange(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                Logger.LogCritical($"{nameof(RemoveByRange)} - The {nameof(entities)} collection is null");
                throw new ArgumentNullException(nameof(entities));
            }

            Logger.LogInformation($"{nameof(RemoveByRange)} - Begin remove of an {nameof(entities)}", entities.ToString());
            var romoveRange = entities as T[] ?? entities.ToArray();
            DbSet.RemoveRange(romoveRange);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await DbSet
               .AsNoTracking()
               .AnyAsync<T>(predicate, cancellationToken);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            return predicate != null
                ? await DbSet.CountAsync(predicate, cancellationToken)
                : await DbSet.CountAsync(cancellationToken);
        }

        public virtual async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var id = await DbContext.SaveChangesAsync(cancellationToken);
            Logger.LogInformation($"{nameof(SaveChangesAsync)} - is saved: {id > 0}");

            var t = DbContext.ChangeTracker.Entries();

            DbContext.Database.CloseConnection();

            return id > 0;
        }


        public async Task<IEnumerable<T>> GetAsync(
                    Expression<Func<T, bool>> filter = null,
                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                    Expression<Func<T, object>>[] includes = null,
                    int? startIndex = null,
                    int? count = null,
                    CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            //Inject Extra Filter If any. Partner Filter for Ex.
            //var extraFilter = this.authService.GetAuthorization<TEntity>();

            //if (extraFilter != null)
            //{
            //    query = query.Where(extraFilter);
            //}

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (startIndex != null)
            {
                query = query.Skip(startIndex ?? 0);
            }

            if (count != null)
            {
                query = query.Take(count ?? 0);
            }

            var list = await query.ToListAsync();

            //bool barExists = typeof(TEntity).GetProperties().Where(x => x.Name == "Template").Any();

            //if (barExists)
            //{
            //    this.PrepareTemplateBody(ref list);
            //}

            return list;
        }

                
    }
}
