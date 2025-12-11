using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearcherApiPrototype_1.Models
{
    public class DocumentModel
    {
        [Key]
        public int Id { get; set; } 
        public string Title { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public byte[] FileData { get; set; }
        [ForeignKey("HardwareInfo")]
        public int HardwareId { get; set; }
        public HardwareInfo Hardware { get; set; }
    }
}
