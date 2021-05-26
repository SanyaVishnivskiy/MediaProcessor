using System;

namespace Core.Business.Records.Models
{
    public class RecordModel
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public RecordFileModel File { get; set; }
        public RecordFileModel Preview { get; set; }
    }
}
