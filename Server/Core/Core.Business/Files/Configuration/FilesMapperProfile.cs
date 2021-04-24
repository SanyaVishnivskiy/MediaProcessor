using AutoMapper;
using Core.Business.Files.Component;
using Core.Business.Files.Component.Models;
using Core.DataAccess.Records.Storage.Models;

namespace Core.Business.Files.Configuration
{
    public class FilesMapperProfile : Profile
    {
        public FilesMapperProfile()
        {
            CreateMap<SaveFileModel, FileModel>()
                .ReverseMap();

            CreateMap<SaveFileResponse, SaveFileResponseModel>()
                .ReverseMap();
        }
    }
}
