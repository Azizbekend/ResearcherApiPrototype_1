using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ResearcherApiPrototype_1.Models
{
    public class StaticObjectInfo
    {
        [Key]
        public int Id { get; set; }
        public string Adress { get; set; }
        public string OperatingOrganization { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string GeneralContractorName { get; set; } = string.Empty;
        public double ProjectEfficiency { get; set; }
        public string PhotoName { get; set; }

        [JsonIgnore]
        public ICollection<ControlBlockInfo> ControlBlocks { get; set; } = new List<ControlBlockInfo>();

    }
}
