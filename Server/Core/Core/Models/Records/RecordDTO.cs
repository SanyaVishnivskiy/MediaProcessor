using Core.Models.Records;
using System;

namespace Core.Models
{
    public class RecordDTO
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public RecordFileDTO File { get; set; }
        public RecordFileDTO Preview { get; set; }
    }
}
