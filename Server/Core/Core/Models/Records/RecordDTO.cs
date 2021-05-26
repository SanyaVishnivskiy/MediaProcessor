using Core.Models.Records;

namespace Core.Models
{
    public class RecordDTO
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public RecordFileDTO File { get; set; }
        public RecordFileDTO Preview { get; set; }
    }
}
