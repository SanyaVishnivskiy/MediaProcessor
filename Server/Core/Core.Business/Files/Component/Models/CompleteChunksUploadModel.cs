namespace Core.Business.Files.Component.Models
{
    public class CompleteChunksUploadModel
    {
        public int ChunksCount { get; set; }
        public string FileId { get; set; }
        public string FileName { get; set; }

        public CompleteChunksUploadModel CloneWithFileName(string name)
        {
            return new CompleteChunksUploadModel
            {
                FileName = name,
                FileId = FileId,
                ChunksCount = ChunksCount
            };
        }
    }
}
