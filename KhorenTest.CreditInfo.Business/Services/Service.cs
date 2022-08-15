
using AutoMapper;
using KhorenTest.CreditInfo.Business.Services.Interfaces;
using KhorenTest.CreditInfo.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace KhorenTest.CreditInfo.Business.Services
{
    public abstract class Service<T, T1> : IService<T, T1>
              where T : class
              where T1 : class
    {
        protected readonly IMapper Mapper;
        protected readonly ILogger Logger;
        protected readonly IRepository<T> Repository;


        protected Service(IMapper mapper, IRepository<T> repository, ILogger logger)
        {
            Mapper = mapper;
            Repository = repository;
            Logger = logger;
        }

        #region T
        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await Repository.AddAsync(entity ?? throw new ArgumentNullException(nameof(entity)), cancellationToken);
            return result;
        }

        public virtual async Task<IReadOnlyCollection<T>> GetAllEntityAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            IReadOnlyCollection<T> entities = (await Repository.GetAllAsync(cancellationToken)).ToList();
            return entities;
        }

        public virtual async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await Repository.FindByAsync(predicate, cancellationToken);
            return result;
        }
        #endregion
        public virtual async Task<T1> AddAsync(T1 entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var dataEntity = Mapper.Map<T>(entity);
            var result = await Repository.AddAsync(dataEntity ?? throw new ArgumentNullException(nameof(entity)), cancellationToken);
            return Mapper.Map<T1>(result);
        }

        public virtual async Task<T1> AddWithSaveChangesAsync(T1 entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var dataEntity = Mapper.Map<T>(entity);

            var result = await Repository.AddAsync(dataEntity ?? throw new ArgumentNullException(nameof(entity)), cancellationToken);
            var isSaveSuccess = await SaveChangesAsync();
            if (!isSaveSuccess)
            {
                return null;
            }
            return Mapper.Map<T1>(result);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T1> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            var dataEntities = Mapper.Map<ICollection<T>>(entities);
            await Repository.AddRangeAsync(dataEntities as IReadOnlyCollection<T>, cancellationToken);
        }

        public virtual async Task<IEnumerable<T1>> FindByAsync(Expression<Func<T1, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            var dataEntitiesPredicate = Mapper.Map<Expression<Func<T, bool>>>(predicate);
            var result = await Repository.FindByAsync(dataEntitiesPredicate, cancellationToken);
            return Mapper.Map<IReadOnlyCollection<T1>>(result);
        }
        public virtual async Task<IEnumerable<T1>> FindByAsync(Expression<Func<T1, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken), params Expression<Func<T, object>>[] includeProperties)
        {
            var dataEntitiesPredicate = Mapper.Map<Expression<Func<T, bool>>>(predicate);
            var result = await Repository.FindByAsync(dataEntitiesPredicate, includeProperties, cancellationToken);
            return Mapper.Map<IReadOnlyCollection<T1>>(result);
        }

        public virtual async Task<IEnumerable<T>> FindEntitiesByAsync(Expression<Func<T, bool>> predicate, bool withTracking = false, CancellationToken cancellationToken = default(CancellationToken), params Expression<Func<T, object>>[] includeProperties)
        {
            var result = withTracking
                ? await Repository.FindByWithTrackingAsync(predicate, cancellationToken, includeProperties)
                : await Repository.FindByAsync(predicate, includeProperties, cancellationToken);
            return result;
        }

        public virtual async Task<IReadOnlyCollection<T1>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = await Repository.GetAllAsync(cancellationToken);
            var businessModelCollection = Mapper.Map<IReadOnlyCollection<T1>>(entities);
            return businessModelCollection;
        }

        public virtual void Remove(T1 entity)
        {
            var dataEntity = Mapper.Map<T>(entity);
            Repository.Remove(dataEntity ?? throw new ArgumentNullException(nameof(entity)));
        }

        public virtual void RemoveByRange(IEnumerable<T1> entities)
        {
            var dataEntities = Mapper.Map<ICollection<T>>(entities);
            Repository.RemoveByRange(dataEntities as IReadOnlyCollection<T>);
        }

        public virtual void Update(T1 entity)
        {
            var dataEntity = Mapper.Map<T>(entity);
            Repository.Update(dataEntity ?? throw new ArgumentNullException(nameof(entity)));
        }

        public virtual void UpdateRange(IEnumerable<T1> entities)
        {
            var dataEntities = Mapper.Map<ICollection<T>>(entities);
            Repository.UpdateRange(dataEntities as IReadOnlyCollection<T>);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T1, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            var dataEntitiesPredicate = Mapper.Map<Expression<Func<T, bool>>>(predicate);
            return await Repository.AnyAsync(dataEntitiesPredicate, cancellationToken);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T1, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            var dataEntitiesPredicate = Mapper.Map<Expression<Func<T, bool>>>(predicate);
            return await Repository.CountAsync(dataEntitiesPredicate, cancellationToken);
        }

        public virtual async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Repository.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task AddIfNotExistsAsync(T1 entity, Expression<Func<T1, bool>> predicate = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            Expression<Func<T, bool>> dataEntitiesPredicate = null;

            if (predicate != null)
                dataEntitiesPredicate = Mapper.Map<Expression<Func<T, bool>>>(predicate);

            var dataEntity = Mapper.Map<T>(entity);
            await Repository.AddIfNotExistsAsync(dataEntity, dataEntitiesPredicate, cancellationToken);
        }

        public virtual async Task AddRangeIfNotExistsAsync(IEnumerable<T1> entities, Func<T, object> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            var dataEntities = Mapper.Map<ICollection<T>>(entities);
            await Repository.AddRangeIfNotExistsAsync(dataEntities, predicate, cancellationToken);
        }


        /*
        public async Task<IEnumerable<T1>> GetAsync(SearchModel search = null, Expression<Func<T, object>>[] includes = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var filter = FiltersHelper.ApplyFilterRules<T>(search.Filters, true);

            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null;

            if (search.Sorting != null)
            {
                if (search.SortingThen == null)
                {
                    orderBy = q => q.OrderingHelper(search.Sorting);
                }
                else
                {
                    orderBy = q => q.OrderingHelper(search.Sorting).OrderingHelper(search.SortingThen, true);
                }
            }

            int? startIndex = null;
            int? count = null;

            if (search.Pageing != null)
            {
                startIndex = search.Pageing.PageSize * (search.Pageing.PageNumber - 1);
                count = search.Pageing.PageSize;
            }

            var entities = await Repository.GetAsync(filter, orderBy, includes, startIndex, count, cancellationToken);
            var businessModelCollection = Mapper.Map<IReadOnlyCollection<T1>>(entities);
            return businessModelCollection;
        }

        public async Task<SearchResultModel<T1>> GetWithCountAsync(SearchModel search = null, Expression<Func<T, object>>[] includes = null, CancellationToken cancellationToken = default)
        {
            SearchResultModel<T1> result = new SearchResultModel<T1>();

            var filter = FiltersHelper.ApplyFilterRules<T>(search.Filters, true);

            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null;

            if (search.Sorting != null)
            {
                if (search.SortingThen == null)
                {
                    orderBy = q => q.OrderingHelper(search.Sorting);
                }
                else
                {
                    orderBy = q => q.OrderingHelper(search.Sorting).OrderingHelper(search.SortingThen, true);
                }
            }

            int? startIndex = null;
            int? count = null;

            if (search.Pageing != null)
            {
                startIndex = search.Pageing.PageSize * (search.Pageing.PageNumber - 1);
                count = search.Pageing.PageSize;
            }

            var entities = await Repository.GetWithCountAsync(filter, orderBy, includes, startIndex, count, cancellationToken);

            result.Data = Mapper.Map<IReadOnlyCollection<T1>>(entities.Data);
            result.Count = entities.Count;

            return result;
        }

        protected async Task<SearchResultModel<T>> GetFilteredDataAsync(SearchModel search = null, Expression<Func<T, object>>[] includes = null, CancellationToken cancellationToken = default)
        {
            SearchResultModel<T> result = new SearchResultModel<T>();

            var filter = FiltersHelper.ApplyFilterRules<T>(search.Filters, true);

            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null;

            if (search.Sorting != null)
            {
                if (search.SortingThen == null)
                {
                    orderBy = q => q.OrderingHelper(search.Sorting);
                }
                else
                {
                    orderBy = q => q.OrderingHelper(search.Sorting).OrderingHelper(search.SortingThen, true);
                }
            }

            int? startIndex = null;
            int? count = null;

            if (search.Pageing != null)
            {
                startIndex = search.Pageing.PageSize * (search.Pageing.PageNumber - 1);
                count = search.Pageing.PageSize;
            }

            var entities = await Repository.GetWithCountAsync(filter, orderBy, includes, startIndex, count, cancellationToken);

            result.Data = entities.Data;
            result.Count = entities.Count;

            return result;
        }


        public async Task<IEnumerable<T1>> GetAsync(
                         Expression<Func<T1, bool>> filter = null,
                         Expression<Func<T, object>>[] includes = null,
                         SearchModel search = null,
                         CancellationToken cancellationToken = default(CancellationToken))
        {
            var filterEntity = Mapper.Map<Expression<Func<T, bool>>>(filter);
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null;

            if (search.Sorting != null)
            {
                if (search.SortingThen == null)
                {
                    orderBy = q => q.OrderingHelper(search.Sorting);
                }
                else
                {
                    orderBy = q => q.OrderingHelper(search.Sorting).OrderingHelper(search.SortingThen, true);
                }
            }

            int? startIndex = null;
            int? count = null;

            if (search.Pageing != null)
            {
                startIndex = search.Pageing.PageSize * (search.Pageing.PageNumber - 1);
                count = search.Pageing.PageSize;
            }

            var entities = await Repository.GetAsync(filterEntity, orderBy, includes, startIndex, count, cancellationToken);
            var businessModelCollection = Mapper.Map<IReadOnlyCollection<T1>>(entities);
            return businessModelCollection;
        }
 */
        public async Task<IEnumerable<T1>> GetAsync(
                           Expression<Func<T1, bool>> filter = null,
                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                           Expression<Func<T, object>>[] includes = null,
                           int? startIndex = null,
                           int? count = null,
                           CancellationToken cancellationToken = default(CancellationToken))
        {

            var _filter = Mapper.Map<Expression<Func<T, bool>>>(filter);

            var entities = await Repository.GetAsync(_filter, orderBy, includes, startIndex, count, cancellationToken);
            var businessModelCollection = Mapper.Map<IReadOnlyCollection<T1>>(entities);
            return businessModelCollection;
        }



    }
}
