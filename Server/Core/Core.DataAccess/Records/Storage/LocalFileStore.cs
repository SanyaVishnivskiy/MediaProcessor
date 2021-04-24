using Core.Common.Models.Configurations;
using Core.DataAccess.Records.DB.Models;
using Core.DataAccess.Records.Storage.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core.DataAccess.Records.Storage
{
    public class LocalFileStore : IFileStore
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

        public Task<string> GetFileLocation(RecordFile file)
        {
            var path = Path.Combine(_options.BaseFilePath, file.RelativePath);
            return Task.FromResult(path);
        }
    }
}
