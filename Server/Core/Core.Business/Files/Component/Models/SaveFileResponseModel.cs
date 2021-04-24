namespace Core.Business.Files.Component
{
    public class SaveFileResponseModel
    {
        public string RelativePath { get; }
        public string FileStoreSchema { get; }

        public SaveFileResponseModel(string relativePath, string fileStoreSchema)
        {
            RelativePath = relativePath;
            FileStoreSchema = fileStoreSchema;
        }
    }
}