using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearcherApiPrototype_1.Models
{
    public class HardwareSchemaImage
    {
        [Key]
        public int Id { get; set; }
        public string  Top { get; set; }
        public string Left { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        [ForeignKey("HardwareSchema")]
        public int HardwareSchemaId { get; set; }
        public HardwareSchema HardwareSchema { get; set; }
        [ForeignKey("FileModel")]
        public int? FileId { get; set; }
        public FileModel? File { get; set; }
    }
}
