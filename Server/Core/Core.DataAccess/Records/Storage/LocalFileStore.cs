using Core.Common.Models.Configurations;
using Core.DataAccess.Records.DB.Models;
using Core.DataAccess.Records.Storage.Models;
using System;
using System.Collections.Generic;
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
            var path = CreateFileLocation(model.FileName);

            return await Save(path, model);
        }

        public async Task<SaveFileResponse> SaveChunk(SaveFileModel model)
        {
            var path = CreateTempFileLocation(model.FileName);

            return await Save(path, model);
        }

        private async Task<SaveFileResponse> Save(string fullPath, SaveFileModel model)
        {
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await model.Stream.CopyToAsync(fileStream);
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
            return CreateFileLocation(file.RelativePath);
        }

        public string CreateFileLocation(string path)
        {
            return Path.Combine(_options.BaseFilePath, path);
        }

        public string CreateTempFileLocation(string path)
        {
            return Path.Combine(_options.TempBaseFilePath, path);
        }

        public Task Delete(RecordFile file)
        {
            var path = GetFileLocation(file);
            return Delete(path);
        }

        private async Task DeleteFiles(List<string> fullPaths)
        {
            foreach (var path in fullPaths)
            {
                await Delete(path);
            }
        }

        private Task Delete(string fullPath)
        {
            File.Delete(fullPath);
            return Task.CompletedTask;
        }

        public async Task<SaveFileResponse> CompleteChunksUpload(CompleteChunksUploadRequest model)
        {
            var resultPath = CreateFileLocation(model.FileName);
            var chunksPaths = GetChunksPaths(model);
            foreach (var chunkPath in chunksPaths)
            {
                await MergeChunks(resultPath, chunkPath);
            }

            await DeleteFiles(chunksPaths);

            return new SaveFileResponse(model.FileName, Schema);
        }

        private List<string> GetChunksPaths(CompleteChunksUploadRequest model)
        {
            var result = new List<string>();
            for (int i = 0; i < model.ChunksCount; i++)
            {
                var chunkPath = CreateTempFileLocation(i + model.FileId);
                if (File.Exists(chunkPath))
                {
                    result.Add(chunkPath);
                }
            }

            return result;
        }

        private static async Task MergeChunks(string chunk1, string chunk2)
        {
            using (var result = File.Open(chunk1, FileMode.Append))
            {
                using (var chunk = File.Open(chunk2, FileMode.Open))
                {
                    var chunkBytes = new byte[chunk.Length];
                    await chunk.ReadAsync(chunkBytes, 0, (int)chunk.Length);
                    await result.WriteAsync(chunkBytes, 0, (int)chunk.Length);
                }
            }
        }
    }
}
