using AutoMapper;
using Core.Business.Records.Models;
using Core.DataAccess.Records.DB.Models;

namespace Core.Business.Records.Configuration
{
    public class RecordsMapperProfile : Profile
    {
        public RecordsMapperProfile()
        {
            CreateMap<Record, RecordModel>()
                .ReverseMap();

            CreateMap<RecordFile, RecordFileModel>()
                .ReverseMap();
        }
    }
}
