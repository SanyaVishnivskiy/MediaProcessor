using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess.Base.Database
{
    public interface IRepository<T>
    {
        Task<T> GetById(string id);
        Task<List<T>> GetAll();
        Task<List<T>> Get(Expression<Func<T, bool>> expression);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task Update(T entity);
        Task Delete(T entity);
        Task DeleteRange(IEnumerable<T> entities);
    }
}
