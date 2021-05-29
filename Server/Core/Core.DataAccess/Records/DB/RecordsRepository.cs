using Core.Common.Models;
using Core.Common.Models.Search;
using Core.DataAccess.Base.Database;
using Core.DataAccess.EF;
using Core.DataAccess.Records.DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess.Records.DB
{
    public class RecordsRepository : GenericRepository<Record>, IRecordsRepository
    {
        public RecordsRepository(AppDbContext context) : base(context)
        {
        }

        public Task<Record> GetByIdAsNoTracking(string id)
        {
            return Set
                .Include(x => x.File)
                .Include(x => x.Preview)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public override Task<Record> GetById(string id)
        {
            return Set
                .Include(x => x.File)
                .Include(x => x.Preview)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<SearchResult<Record>> GetWithAllDependencies(RecordSearchContext context)
        {
            var orderByFunc = GetOrderByFunc(context);

            return Get(
                new FuncPagination<Record>(context.Pagination, orderByFunc),
                query => query
                    .Where(x => x.FileName.Contains(context.Search))
                    .Include(x => x.File)
                    .Include(x => x.Preview)
                    .AsNoTracking());
        }

        private Expression<Func<Record, object>> GetOrderByFunc(RecordSearchContext context)
        {
            var orderBy = context.Pagination.SortBy;
            if (orderBy == "createdOn")
            {
                return x => x.CreatedOn;
            }
            if (orderBy == "fileName")
            {
                return x => x.FileName;
            }

            return x => x.Id;
        }

        public override Task Delete(Record entity)
        {
            Set.Remove(entity);
            Context.Files.Remove(entity.File);

            if (IsPreviewSameFile(entity))
            {
                Context.Files.Remove(entity.Preview);
            }

            return Task.CompletedTask;
        }

        private bool IsPreviewSameFile(Record entity)
        {
            return entity.Preview?.Id != null && entity.Preview.Id != entity.File.Id;
        }

        public async Task DeletePreview(string id)
        {
            var record = await GetById(id);
            var preview = record.Preview;
            record.Preview = null;
            Context.Files.Remove(preview);
        }
    }
}
