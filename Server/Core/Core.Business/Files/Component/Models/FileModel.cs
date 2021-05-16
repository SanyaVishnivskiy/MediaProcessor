using System.IO;

namespace Core.Business.Files.Component.Models
{
    public class FileModel
    {
        public string FileName { get; set; }
        public Stream Stream { get; set; }

        public FileModel CloneWithFileName(string name)
        {
            return new FileModel
            {
                FileName = name,
                Stream = Stream
            };
        }
    }
}
