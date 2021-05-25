using AutoMapper;
using Core.Business.Auth.Models;
using Core.DataAccess.Auth;

namespace Core.Business.Auth.Configuration
{
    public class AutomapperAuthProfile : Profile
    {
        public AutomapperAuthProfile()
        {
            CreateMap<User, UserModel>()
                .ReverseMap();

            CreateMap<CreateUserModel, User>();
            CreateMap<UpdateUserModel, User>();
        }
    }
}
