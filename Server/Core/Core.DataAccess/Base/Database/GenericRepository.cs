using Core.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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

        public virtual Task<List<T>> Get(Expression<Func<T, bool>> expression)
        {
            return Set.Where(expression).ToListAsync();
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
    }
}
