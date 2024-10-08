﻿using AutoMapper;
using Core.Business.Files.Component.Models;
using Core.Business.Records.Models;
using Core.DataAccess.Records.DB.Models;
using Core.DataAccess.Records.Storage.Models;
using System;
using System.IO;
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

        public async Task<Stream> Download(RecordFileModel file)
        {
            var store = _storeFactory.Create(file.FileStoreSchema);
            var recordFile = _mapper.Map<RecordFile>(file);
            return await store.Download(recordFile);
        }

        public async Task<SaveFileResponseModel> Save(FileModel file)
        {
            var store = _storeFactory.CreateDefault();
            var result = await store.Save(_mapper.Map<SaveFileModel>(file));
            return _mapper.Map<SaveFileResponseModel>(result);
        }

        public async Task<SaveFileResponseModel> SaveChunk(FileModel file)
        {
            var store = _storeFactory.CreateDefault();
            var result = await store.SaveChunk(_mapper.Map<SaveFileModel>(file));
            return _mapper.Map<SaveFileResponseModel>(result);
        }

        public async Task<SaveFileResponseModel> CompleteChunksUpload(CompleteChunksUploadModel model)
        {
            var store = _storeFactory.CreateDefault();
            var request = _mapper.Map<CompleteChunksUploadRequest>(model);
            var result = await store.CompleteChunksUpload(request);
            return _mapper.Map<SaveFileResponseModel>(result);
        }

        public async Task Delete(RecordFileModel file)
        {
            var store = _storeFactory.CreateDefault();
            await store.Delete(_mapper.Map<RecordFile>(file));
        }
    }
}
