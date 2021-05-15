using Core.Common.Models.Configurations;
using Core.DataAccess.Records.Storage;

namespace FileProcessor.Files
{
    public interface IActionsFileStore : ILocalFileStore
    {
    }

    public class LocalFileStoreAdapter : LocalFileStore, IActionsFileStore
    {
        public LocalFileStoreAdapter(LocalFileStoreOptions options)
            : base(options)
        {
        }
    }
}
