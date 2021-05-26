using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DataAccess.Records.DB.Models
{
    public class Record
    {
        [MaxLength(50)]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FileName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [MaxLength(50)]
        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public RecordFile File { get; set; }

        public RecordFile Preview { get; set; }
    }
}
