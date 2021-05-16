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
            return new RecordModel
            {
                FileName = file.FileName,
                File = new RecordFileModel
                {
                    FileStoreSchema = response.FileStoreSchema,
                    RelativePath = response.RelativePath
                }
            };
        }

        public async Task Delete(string id)
        {
            var record = await GetById(id);
            await _filesComponent.Delete(record.File);
            await _recordComponent.Delete(id);
        }
    }
}
