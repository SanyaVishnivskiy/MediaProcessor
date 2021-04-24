using AutoMapper;
using Core.Business.Files.Component;
using Core.Business.Records.Models;
using Core.Models;
using Core.Models.Records;
using System;

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
        }
    }
}
