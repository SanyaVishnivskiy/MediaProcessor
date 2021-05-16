using Core.DataAccess.Records.DB.Models;
using Core.DataAccess.Records.Storage.Models;
using System.IO;
using System.Threading.Tasks;

namespace Core.DataAccess.Records.Storage
{
    public interface IFileStore
    {
        string Schema { get; }
        Task<SaveFileResponse> Save(SaveFileModel model);
        Task<Stream> Download(RecordFile recordFile);
        Task Delete(RecordFile recordFile);
    }
}
