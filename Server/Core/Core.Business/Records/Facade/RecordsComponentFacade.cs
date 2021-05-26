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
        private readonly IRecordJobComponent _jobComponent;

        public RecordsComponentFacade(
            IRecordsComponent recordComponent,
            IFilesComponent filesComponent,
            IRecordJobComponent jobComponent)
        {
            _recordComponent = recordComponent ?? throw new ArgumentNullException(nameof(recordComponent));
            _filesComponent = filesComponent ?? throw new ArgumentNullException(nameof(filesComponent));
            _jobComponent = jobComponent ?? throw new ArgumentNullException(nameof(jobComponent));
        }

        public Task<SearchResult<RecordModel>> Get(RecordSearchContext context)
        {
            return _recordComponent.GetWithDependencies(context);
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

            await AddRecord(response, file);
        }

        private async Task AddRecord(SaveFileResponseModel response, FileModel file)
        {
            var record = MapToRecord(response, file);
            record.TrySetPreview();
            await _recordComponent.AddDefault(record);

            await SubmitPreviewGenerationIfNeeded(record);
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
                Id = Guid.NewGuid().ToString(),
                FileName = fileName,
                File = new RecordFileModel
                {
                    Id = Guid.NewGuid().ToString(),
                    FileStoreSchema = response.FileStoreSchema,
                    RelativePath = response.RelativePath
                }
            };
        }

        private Task SubmitPreviewGenerationIfNeeded(RecordModel record)
        {
            if (record.Preview != null)
            {
                return Task.CompletedTask;
            }

            return _jobComponent.SubmitPreviewGeneration(record);
        }

        public Task<SaveFileResponseModel> SaveFileChunk(FileModel file)
        {
            return _filesComponent.SaveChunk(file);
        }

        public async Task CompleteChunksUpload(CompleteChunksUploadModel model)
        {
            var cloned = model.CloneWithFileName(RandomFileName(model.FileName));
            var result = await _filesComponent.CompleteChunksUpload(cloned);
            await AddRecord(result, FileModel.From(model));
        }

        public async Task Delete(string id)
        {
            var record = await GetById(id);
            await _recordComponent.Delete(id);
            await _filesComponent.Delete(record.File);
            await _filesComponent.Delete(record.Preview);
        }

        public async Task SetPreview(string recordId, FileModel file)
        {
            var record = await GetById(recordId);
            var preview = record.Preview;
            var response = await SaveFile(file);
            await SetPreviewForRecord(record, response);

            await _filesComponent.Delete(preview);
        }

        private Task<SaveFileResponseModel> SaveFile(FileModel file)
        {
            return _filesComponent.Save(file);
        }

        private async Task SetPreviewForRecord(RecordModel record, SaveFileResponseModel response)
        {
            var recordFromSavedFile = MapToRecord(response, "");
            record.Preview = recordFromSavedFile.File;
            await _recordComponent.Update(record);
        }
    }
}
