using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResearcherApiPrototype_1.Models
{
    public class HardwareInfo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string DeveloperName { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public string PhotoName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string OpcDescription { get; set; } = string.Empty;
        [ForeignKey("ControlBlockInfo")]
        public int ControlBlockId { get; set; }
        [JsonIgnore]
        public ControlBlockInfo ControlBlock { get; set; } = null!;
        [JsonIgnore]
        public ICollection<NodeInfo> NodeInfos { get; set; } = new List<NodeInfo>();
        [JsonIgnore]
        public ICollection<HardwareCharacteristic> Characteristics { get; set; }
    }
}
