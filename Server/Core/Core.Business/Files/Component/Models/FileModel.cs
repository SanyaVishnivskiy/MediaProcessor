using System.IO;

namespace Core.Business.Files.Component.Models
{
    public class FileModel : FileModelStream
    {
        public string FileName { get; set; }

        public FileModel CloneWithFileName(string name)
        {
            return new FileModel
            {
                FileName = name,
                Stream = Stream
            };
        }

        public static FileModel From(CompleteChunksUploadModel model)
        {
            return new FileModel
            {
                FileName = model.FileName
            };
        }
    }

    public class FileModelStream
    {
        public Stream Stream { get; set; }
    }
}
