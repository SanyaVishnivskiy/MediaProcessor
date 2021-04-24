using AutoMapper;
using Core.Business.Files.Component.Models;
using Core.Business.Records.Models;
using Core.DataAccess.Records.DB.Models;
using Core.DataAccess.Records.Storage;
using Core.DataAccess.Records.Storage.Models;
using System;
using System.Threading.Tasks;

namespace Core.Business.Files.Component
{
    public class FilesComponent : IFilesComponent
    {
        private readonly IFileStoreFactory _storeFactory;
        private readonly IMapper _mapper;

        public FilesComponent(IFileStoreFactory storeFactory, IMapper mapper)
        {
            _storeFactory = storeFactory ?? throw new ArgumentNullException(nameof(storeFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<string> GetFileLocation(RecordFileModel file)
        {
            var store = _storeFactory.Create(file.FileStoreSchema);
            var recordFile = _mapper.Map<RecordFile>(file);
            return store.GetFileLocation(recordFile);
        }

        public async Task<SaveFileResponseModel> Save(FileModel file)
        {
            var store = _storeFactory.CreateDefault();
            var result = await store.Save(_mapper.Map<SaveFileModel>(file));
            return _mapper.Map<SaveFileResponseModel>(result);
        }
    }
}
