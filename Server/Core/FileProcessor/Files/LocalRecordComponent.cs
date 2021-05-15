using Core.Business.Files.Component;
using Core.Business.Files.Component.Models;
using Core.Business.Records.Component;
using Core.Business.Records.Models;
using Core.Common.Exceptions;
using Core.DataAccess.Records.DB.Models;
using Core.DataAccess.Records.Storage;
using Core.DataAccess.Records.Storage.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileProcessor.Files
{
    public interface ILocalRecordsComponent
    {
        Task<DownloadingResult> DownloadLocally(string recordId);
        Task AddRecord(string fileName, string localFilePath);
        Task DeleteLocally(string path);
    }

    public class LocalRecordComponent : ILocalRecordsComponent
    {
        private readonly IRecordsComponent _component;
        private readonly IFilesComponent _filesComponent;
        private readonly ILocalFileStore _store;

        public LocalRecordComponent(
            IRecordsComponent component,
            IFilesComponent filesComponent,
            IActionsFileStore store)
        {
            _component = component ?? throw new ArgumentNullException(nameof(component));
            _filesComponent = filesComponent ?? throw new ArgumentNullException(nameof(filesComponent));
            _store = store ?? throw new ArgumentNullException(nameof(store));
        }

        public async Task<DownloadingResult> DownloadLocally(string recordId)
        {
            var record = await _component.GetById(recordId);
            if (record is null)
                throw new ItemNotFoundException($"Record {recordId} does not exists");

            var fileStream = await _filesComponent.Download(record.File);

            var path = await Save(fileStream, record);
            return new DownloadingResult(record, path);
        }

        private async Task<string> Save(Stream stream, RecordModel record)
        {
            var fileName = BuildNewFileName(record);
            var saved = await _store.Save(new SaveFileModel(fileName, stream));

            return _store.GetFileLocation(new RecordFile
            {
                RelativePath = saved.RelativePath
            });
        }

        private string BuildNewFileName(RecordModel record)
        {
            return Guid.NewGuid().ToString() + Path.GetExtension(record.FileName);
        }

        public async Task AddRecord(string fileName, string localFilePath)
        {
            SaveFileResponseModel saveResult = null;
            using (var stream = File.OpenRead(localFilePath))
            {
                saveResult = await _filesComponent.Save(BuildFileModel(stream, fileName));
            }

            await _component.AddDefault(BuildRecord(fileName, saveResult));
        }

        private FileModel BuildFileModel(Stream stream, string fileName)
        {
            return new FileModel
            {
                FileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName),
                Stream = stream,
            };
        }

        private RecordModel BuildRecord(string fileName, SaveFileResponseModel saveResult)
        {
            return new RecordModel
            {
                FileName = fileName,
                File = new RecordFileModel
                {
                    FileStoreSchema = saveResult.FileStoreSchema,
                    RelativePath = saveResult.RelativePath
                }
            };
        }

        public Task DeleteLocally(string path)
        {
            File.Delete(path);
            return Task.CompletedTask;
        }
    }
}
