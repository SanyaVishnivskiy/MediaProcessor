using Microsoft.AspNetCore.Http;

namespace Core.Models.Files
{
    public class FileChunkDTO
    {
        public string FileId { get; set; }
        public int Number { get; set; }
        public IFormFile File { get; set; }
    }
}
