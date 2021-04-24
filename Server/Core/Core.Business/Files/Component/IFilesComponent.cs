using Core.Business.Records.Models;
using Core.DataAccess.Records.DB.Models;
using System.Threading.Tasks;

namespace Core.Business.Files.Component.Models
{
    public interface IFilesComponent
    {
        Task<string> GetFileLocation(RecordFileModel file);
        Task<SaveFileResponseModel> Save(FileModel file);
    }
}
