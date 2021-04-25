using AutoMapper;
using Core.Business.Files.Component;
using Core.Business.Files.Component.Models;
using Core.Business.Records.Component;
using Core.Business.Records.Models;
using Core.Models.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IFilesComponent _component;
        private readonly IRecordsComponent _recordsComponent;
        private readonly IMapper _mapper;

        public FilesController(
            IFilesComponent component,
            IRecordsComponent recordsComponent,
            IMapper mapper)
        {
            _component = component;
            _recordsComponent = recordsComponent;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] FileDTO file)
        {
            var response = await SaveFile(file);

            var record = MapToRecord(response, file);
            await _recordsComponent.AddDefault(record);

            return StatusCode(StatusCodes.Status201Created);
        }

        private RecordModel MapToRecord(SaveFileResponseModel response, FileDTO file)
        {
            return new RecordModel
            {
                FileName = file.FileName,
                File = new RecordFileModel
                {
                    FileStoreSchema = response.FileStoreSchema,
                    RelativePath = response.RelativePath
                }
            };
        }

        private async Task<SaveFileResponseModel> SaveFile(FileDTO file)
        {
            using (var stream = file.FormFile.OpenReadStream())
            {
                return await _component.Save(new FileModel{
                    FileName = file.FileName,
                    Stream = stream
                });
            }
        }
    }
}
