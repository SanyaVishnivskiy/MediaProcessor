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

        public RecordFile File { get; set; }

        public RecordFile Preview { get; set; }
    }
}
