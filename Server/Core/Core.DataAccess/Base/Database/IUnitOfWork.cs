using Core.DataAccess.Records.DB;
using System.Threading.Tasks;

namespace Core.DataAccess.Base.Database
{
    public interface IUnitOfWork
    {
        IRecordsRepository Records { get; }

        Task SaveChanges();
    }
}
