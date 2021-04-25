using Core.Business.Records.Models;
using System.IO;
using System.Threading.Tasks;

namespace Core.Business.Files.Component.Models
{
    public interface IFilesComponent
    {
        Task<SaveFileResponseModel> Save(FileModel file);
        Task<Stream> Download(RecordFileModel file);
    }
}
