using Microsoft.AspNetCore.Http;

namespace Core.Models.Files
{
    public class FileDTO
    {
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
