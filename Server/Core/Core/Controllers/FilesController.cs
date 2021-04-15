using Core.Models.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] FileModel file)
        {
            try
            {
                await SaveFile(file);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        private async Task SaveFile(FileModel file)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.FormFile.CopyToAsync(stream);
            }
        }
    }
}
