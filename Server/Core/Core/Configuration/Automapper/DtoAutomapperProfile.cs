using AutoMapper;
using Core.Business.Auth.Models;
using Core.Business.Files.Component.Models;
using Core.Business.Models;
using Core.Business.Records.Models;
using Core.Models;
using Core.Models.Auth;
using Core.Models.Files;
using Core.Models.Records;

namespace Core.Configuration.Automapper
{
    public class DtoAutomapperProfile : Profile
    {
        public DtoAutomapperProfile()
        {
            CreateMap<RecordModel, RecordDTO>()
                .ReverseMap();

            CreateMap<RecordFileModel, RecordFileDTO>()
                .ReverseMap();

            CreateMap<CompleteChunksUploadModel, CompleteChunksUploadDTO>()
                .ReverseMap();

            CreateMap<CreateUserModel, CreateUserDTO>()
                .ReverseMap();

            CreateMap<UserModel, UserDTO>()
                .ReverseMap();

            CreateMap<LoginModel, LoginDTO>()
                .ReverseMap();

            CreateMap<UpdateUserModel, UpdateUserDTO>()
                .ReverseMap();

            CreateMap<SearchUsersContext, SearchUserContextDTO>()
                .ReverseMap();
        }
    }
}
