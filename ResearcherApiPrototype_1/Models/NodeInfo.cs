using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResearcherApiPrototype_1.Models
{
    public class NodeInfo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mesurement { get; set; } = string.Empty; //кв.ч, м3 и так далее
        public string PlcNodeId { get; set; }
        public bool IsCommand { get; set; } = false;
        public bool IsValue { get; set; } = true;
        public string? MinValue { get; set; }
        public string? MaxValue { get; set; }
        public string? LastValue { get; set; }
        [ForeignKey("HardwareInfo")]
        public int HardwareId { get; set; }
        [JsonIgnore]
        public HardwareInfo HardwareInfo { get; set; } = null!;
    }
}
