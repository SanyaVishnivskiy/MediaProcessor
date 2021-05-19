namespace Core.DataAccess.Records.Storage.Models
{
    public class CompleteChunksUploadRequest
    {
        public int ChunksCount { get; set; }
        public string FileId { get; set; }
        public string FileName { get; set; }
    }
}
