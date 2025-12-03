using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResearcherApiPrototype_1.Models
{
    public class ControlBlockInfo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PlcIpAdress { get; set; } = string.Empty;
        [ForeignKey("StaticObjectInfos")]
        public int StaticObjectInfoId { get; set; }
        public StaticObjectInfo StaticObjectInfo { get; set; }

        [JsonIgnore]
        public ICollection<HardwareInfo> HardwareInfo { get; set; } = new List<HardwareInfo>();

       
    }
}
