using System.ComponentModel.DataAnnotations;

namespace Core.DataAccess.Records.DB.Models
{
    public class RecordFile
    {
        [Required]
        [MaxLength(50)]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FileStoreSchema { get; set; }

        [Required]
        [MaxLength(150)]
        public string RelativePath { get; set; }
    }
}
