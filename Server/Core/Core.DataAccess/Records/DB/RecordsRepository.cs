using Core.Common.Models;
using Core.Common.Models.Search;
using Core.DataAccess.Base.Database;
using Core.DataAccess.EF;
using Core.DataAccess.Records.DB.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Core.DataAccess.Records.DB
{
    public class RecordsRepository : GenericRepository<Record>, IRecordsRepository
    {
        public RecordsRepository(AppDbContext context) : base(context)
        {
        }

        public override Task<Record> GetById(string id)
        {
            return Set
                .Include(x => x.File)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<SearchResult<Record>> GetWithAllDependencies(Pagination pagination)
        {
            return Get(
                pagination,
                query => query.Include(x => x.File));
        }
    }
}
