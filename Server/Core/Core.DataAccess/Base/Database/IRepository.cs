using Core.Common.Models;
using Core.Common.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DataAccess.Base.Database
{
    public interface IRepository<T>
    {
        Task<T> GetById(string id);
        Task<List<T>> GetAll();
        Task<SearchResult<T>> Get(
            FuncPagination<T> pagination,
            Func<IQueryable<T>, IQueryable<T>> modify = null);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task Update(T entity);
        Task Delete(string id);
        Task Delete(T entity);
        Task DeleteRange(IEnumerable<T> entities);
    }
}
