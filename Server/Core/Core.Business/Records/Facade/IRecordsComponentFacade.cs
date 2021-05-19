using Core.Business.Files.Component;
using Core.Business.Files.Component.Models;
using Core.Business.Records.Models;
using Core.Common.Models;
using Core.Common.Models.Search;
using System.IO;
using System.Threading.Tasks;

namespace Core.Business.Records.Facade
{
    public interface IRecordsComponentFacade
    {
        Task<SearchResult<RecordModel>> Get(Pagination pagination);
        Task<RecordModel> GetById(string id);
        Task<Stream> Download(RecordModel record);
        Task<SaveFileResponseModel> SaveFileChunk(FileModel file);
        Task CompleteChunksUpload(CompleteChunksUploadModel model);
        Task Create(FileModel file);
        Task Update(RecordModel model);
        Task Delete(string id);
    }
}
