using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResearcherApiPrototype_1.Models
{
    public class HardwareSchema
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SchemaImage { get; set; }
        [ForeignKey("StaticObjectInfo")]
        public int StaticObjectInfoId { get; set; }
        public StaticObjectInfo StaticObjectInfo { get; set; }

        [JsonIgnore]
        public ICollection<HardwareSchemaImage> Images { get; set; } = new List<HardwareSchemaImage>();
    }
}
