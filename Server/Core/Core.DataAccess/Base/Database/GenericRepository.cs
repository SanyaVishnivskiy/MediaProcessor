using Core.Common.Models;
using Core.Common.Models.Search;
using Core.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DataAccess.Base.Database
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected AppDbContext Context { get; }
        protected DbSet<T> Set { get; }

        public GenericRepository(AppDbContext context)
        {
            Context = context;
            Set = context.Set<T>();
        }

        public virtual async Task Add(T entity)
        {
             await Set.AddAsync(entity);
        }

        public virtual async Task AddRange(IEnumerable<T> entities)
        {
            await Set.AddRangeAsync(entities);
        }

        public virtual async Task<SearchResult<T>> Get(
            FuncPagination<T> pagination,
            Func<IQueryable<T>, IQueryable<T>> modify = null)
        {
            var query = modify?.Invoke(Set) ?? Set;
            query = query.AsNoTracking();

            var results = await Paginate(query, pagination).ToListAsync();
            var count = await query.CountAsync();

            return new SearchResult<T>
            {
                Items = results,
                Page = pagination.Page,
                Size = pagination.Size,
                TotalItems = count
            };
        }

        public virtual Task<List<T>> GetAll()
        {
            return Set.ToListAsync();
        }

        public virtual Task<T> GetById(string id)
        {
            return Set.FindAsync(id).AsTask();
        }

        public virtual Task Update(T entity)
        {
            Set.Update(entity);
            return Task.CompletedTask;
        }

        public virtual async Task Delete(string id)
        {
            var record = await GetById(id);
            await Delete(record);
        }

        public virtual Task Delete(T entity)
        {
            Set.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task DeleteRange(IEnumerable<T> entities)
        {
            Set.RemoveRange(entities);
            return Task.CompletedTask;
        }

        protected IQueryable<T> Paginate(
            IQueryable<T> items,
            FuncPagination<T> pagination)
        {
            var ordered = GetOrderByQuery(items, pagination);

            return ordered
                .Skip((pagination.Page - 1) * pagination.Size)
                .Take(pagination.Size);
        }

        private IQueryable<T> GetOrderByQuery(IQueryable<T> items, FuncPagination<T> pagination)
        {
            if (pagination.IsDescendingSortOrder)
            {
                return items.OrderByDescending(pagination.SortBy);
            }

            return items.OrderBy(pagination.SortBy);
        }
    }
}
