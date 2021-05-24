using Core.Business.Files.Component.Models;
using Core.Business.Records.Facade;
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
        private readonly IRecordsComponentFacade _facade;
        private readonly ILocalFileStore _store;

        public LocalRecordComponent(
            IRecordsComponentFacade facade,
            IActionsFileStore store)
        {
            _facade = facade ?? throw new ArgumentNullException(nameof(facade));
            _store = store ?? throw new ArgumentNullException(nameof(store));
        }

        public async Task<DownloadingResult> DownloadLocally(string recordId)
        {
            var record = await _facade.GetById(recordId);
            if (record is null)
                throw new ItemNotFoundException($"Record {recordId} does not exists");

            using (var stream = await _facade.Download(record))
            {
                var path = await Save(stream, record);
                return new DownloadingResult(record, path);
            }
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
            return Guid.NewGuid().ToString() + Path.GetExtension(record.File.RelativePath);
        }

        public async Task AddRecord(string fileName, string localFilePath)
        {
            using (var stream = File.OpenRead(localFilePath))
            {
                await _facade.Create(BuildFileModel(stream, fileName));
            }
        }

        private FileModel BuildFileModel(Stream stream, string fileName)
        {
            return new FileModel
            {
                FileName = fileName,
                Stream = stream,
            };
        }

        public Task DeleteLocally(string path)
        {
            File.Delete(path);
            return Task.CompletedTask;
        }
    }
}
