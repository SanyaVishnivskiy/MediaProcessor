namespace Core.Business.Records.Models
{
    public class RecordModel
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public RecordFileModel File { get; set; }
        public RecordFileModel Preview { get; set; }
    }
}
