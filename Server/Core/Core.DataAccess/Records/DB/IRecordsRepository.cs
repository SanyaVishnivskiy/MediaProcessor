using Core.Common.Models;
using Core.Common.Models.Search;
using Core.DataAccess.Base.Database;
using Core.DataAccess.Records.DB.Models;
using System.Threading.Tasks;

namespace Core.DataAccess.Records.DB
{
    public interface IRecordsRepository : IRepository<Record>
    {
        Task<SearchResult<Record>> GetWithAllDependencies(RecordSearchContext context);
        Task<Record> GetByIdAsNoTracking(string id);
        Task DeletePreview(string id);
    }
}
