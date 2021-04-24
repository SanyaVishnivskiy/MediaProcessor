using Core.DataAccess.Records.Storage;
using System.Collections.Generic;
using System.Linq;

namespace Core.Business.Files.Component
{
    public interface IFileStoreFactory
    {
        IFileStore CreateDefault();
        IFileStore Create(string schema);
    }

    public class FileStoreFactory : IFileStoreFactory
    {
        private readonly Dictionary<string, IFileStore> _map;

        public FileStoreFactory(IEnumerable<IFileStore> stores)
        {
            _map = stores.ToDictionary(x => x.Schema);
        }

        public IFileStore CreateDefault()
        {
            return _map["local"];
        }

        public IFileStore Create(string schema)
        {
            if (_map.TryGetValue(schema, out var store))
            {
                return store;
            }

            return null;
        }
    }
}
