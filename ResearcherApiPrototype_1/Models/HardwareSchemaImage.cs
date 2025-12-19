using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public HardwareSchema HardwareSchema { get; set; }
        [ForeignKey("FileModel")]
        public int? FileId { get; set; }
        [JsonIgnore]
        public FileModel? File { get; set; }
        public int HardwareId { get; set; }
        public int? RedFileId { get; set; }
        public int? GreenFileId { get; set; }

    }
}
