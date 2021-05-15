using Core.Business.Records.Models;

namespace FileProcessor.Files
{
    public class DownloadingResult
    {
        public RecordModel Record { get; }
        public string LocalPath { get; }

        public DownloadingResult(RecordModel record, string path)
        {
            Record = record;
            LocalPath = path;
        }
    }
}