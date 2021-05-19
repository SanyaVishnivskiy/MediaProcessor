﻿using AutoMapper;
using Core.Business.Files.Component.Models;
using Core.Business.Records.Models;
using Core.Models;
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
        }
    }
}
