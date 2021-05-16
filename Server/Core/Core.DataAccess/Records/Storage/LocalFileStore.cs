using Core.Common.Models.Configurations;
using Core.DataAccess.Records.DB.Models;
using Core.DataAccess.Records.Storage.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core.DataAccess.Records.Storage
{
    public interface ILocalFileStore : IFileStore
    {
        string GetFileLocation(RecordFile file);
    }

    public class LocalFileStore : ILocalFileStore
    {
        public string Schema => "local";

        private readonly LocalFileStoreOptions _options;

        public LocalFileStore(LocalFileStoreOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<SaveFileResponse> Save(SaveFileModel model)
        {
            var path = Path.Combine(_options.BaseFilePath, model.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.Stream.CopyToAsync(stream);
            }

            return new SaveFileResponse(model.FileName, Schema);
        }

        public Task<Stream> Download(RecordFile file)
        {
            var path = GetFileLocation(file);
            var stream = File.OpenRead(path);
            return Task.FromResult((Stream)stream);
        }

        public string GetFileLocation(RecordFile file)
        {
            return Path.Combine(_options.BaseFilePath, file.RelativePath);
        }

        public Task Delete(RecordFile file)
        {
            var path = GetFileLocation(file);
            File.Delete(path);
            return Task.CompletedTask;
        }
    }
}
