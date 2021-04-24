using Core.DataAccess.EF;
using Core.DataAccess.Records.DB;
using System.Threading.Tasks;

namespace Core.DataAccess.Base.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(
            AppDbContext context,
            IRecordsRepository recordsRepo)
        {
            _context = context;
            Records = recordsRepo;
        }

        public IRecordsRepository Records { get; }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }
    }
}
