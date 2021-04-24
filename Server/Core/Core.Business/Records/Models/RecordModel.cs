namespace Core.Business.Records.Models
{
    public class RecordModel
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public RecordFileModel File { get; set; }
    }
}
