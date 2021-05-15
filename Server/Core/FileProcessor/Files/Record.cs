namespace FileProcessor.Actions
{
    public class Record
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public RecordFile File { get; set; }
    }

    public class RecordFile
    {
        public string Id { get; set; }
        public string FileStoreSchema { get; set; }
        public string RelativePath { get; set; }
    }
}