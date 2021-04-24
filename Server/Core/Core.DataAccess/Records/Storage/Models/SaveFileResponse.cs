namespace Core.DataAccess.Records.Storage.Models
{
    public class SaveFileResponse
    {
        public string RelativePath { get; }
        public string FileStoreSchema { get; }

        public SaveFileResponse(string relativePath, string fileStoreSchema)
        {
            RelativePath = relativePath;
            FileStoreSchema = fileStoreSchema;
        }
    }
}
