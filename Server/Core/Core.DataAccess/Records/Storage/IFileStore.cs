using Core.DataAccess.Records.DB.Models;
using Core.DataAccess.Records.Storage.Models;
using System.Threading.Tasks;

namespace Core.DataAccess.Records.Storage
{
    public interface IFileStore
    {
        string Schema { get; }
        Task<string> GetFileLocation(RecordFile file);
        Task<SaveFileResponse> Save(SaveFileModel model);
    }
}
