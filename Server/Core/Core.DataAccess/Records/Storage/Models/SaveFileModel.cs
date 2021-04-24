using System.IO;

namespace Core.DataAccess.Records.Storage.Models
{
    public class SaveFileModel
    {
        public string FileName { get; set; }
        public Stream Stream { get; set; }
    }
}
