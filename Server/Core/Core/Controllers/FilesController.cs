using AutoMapper;
using Core.Business.Files.Component.Models;
using Core.Business.Records.Facade;
using Core.Models.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IRecordsComponentFacade _facade;
        private readonly IMapper _mapper;

        public FilesController(
            IRecordsComponentFacade facade,
            IMapper mapper)
        {
            _facade = facade;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] FileDTO file)
        {
            using (var stream = file.FormFile.OpenReadStream())
            {
                await _facade.Create(FormFileModel(file, stream));
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        private FileModel FormFileModel(FileDTO file, System.IO.Stream stream)
        {
            return new FileModel
            {
                FileName = file.FileName,
                Stream = stream
            };
        }
    }
}
