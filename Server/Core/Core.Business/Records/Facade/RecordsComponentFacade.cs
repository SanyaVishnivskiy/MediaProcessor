using Core.Business.Files.Component;
using Core.Business.Files.Component.Models;
using Core.Business.Records.Component;
using Core.Business.Records.Models;
using Core.Common.Models;
using Core.Common.Models.Search;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core.Business.Records.Facade
{
    public class RecordsComponentFacade : IRecordsComponentFacade
    {
        private readonly IRecordsComponent _recordComponent;
        private readonly IFilesComponent _filesComponent;

        public RecordsComponentFacade(
            IRecordsComponent recordComponent,
            IFilesComponent filesComponent)
        {
            _recordComponent = recordComponent ?? throw new ArgumentNullException(nameof(recordComponent));
            _filesComponent = filesComponent ?? throw new ArgumentNullException(nameof(filesComponent));
        }

        public Task<SearchResult<RecordModel>> Get(Pagination pagination)
        {
            return _recordComponent.GetWithDependencies(pagination);
        }

        public Task<RecordModel> GetById(string id)
        {
            return _recordComponent.GetById(id);
        }

        public Task<Stream> Download(RecordModel record)
        {
            return _filesComponent.Download(record.File);
        }

        public Task Update(RecordModel model)
        {
            return _recordComponent.Update(model);
        }

        public async Task Create(FileModel file)
        {
            var cloned = file.CloneWithFileName(RandomFileName(file.FileName));
            var response = await _filesComponent.Save(cloned);

            var record = MapToRecord(response, file);
            await _recordComponent.AddDefault(record);
        }

        private string RandomFileName(string fileName)
        {
            return Guid.NewGuid().ToString() + Path.GetExtension(fileName);
        }

        private RecordModel MapToRecord(SaveFileResponseModel response, FileModel file)
        {
            return MapToRecord(response, file.FileName);
        }

        private RecordModel MapToRecord(SaveFileResponseModel response, string fileName)
        {
            return new RecordModel
            {
                FileName = fileName,
                File = new RecordFileModel
                {
                    FileStoreSchema = response.FileStoreSchema,
                    RelativePath = response.RelativePath
                }
            };
        }

        public Task<SaveFileResponseModel> SaveFileChunk(FileModel file)
        {
            return _filesComponent.SaveChunk(file);
        }

        public async Task CompleteChunksUpload(CompleteChunksUploadModel model)
        {
            var cloned = model.CloneWithFileName(RandomFileName(model.FileName));
            var result = await _filesComponent.CompleteChunksUpload(cloned);
            var record = MapToRecord(result, model.FileName);
            await _recordComponent.AddDefault(record);
        }

        public async Task Delete(string id)
        {
            var record = await GetById(id);
            await _filesComponent.Delete(record.File);
            await _recordComponent.Delete(id);
        }
    }
}
