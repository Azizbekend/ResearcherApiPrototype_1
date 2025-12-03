using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResearcherApiPrototype_1.Models
{
    public class MaintenanceRequest
    {
        [Key]
        public int Id { get; set; }
        public string Discription { get; set; }
        public int Period { get; set; } //days
        public DateTime NextMaintenanceDate { get; set; } 

        [ForeignKey("HardwareInfo")]
        public int HardwareId { get; set; }
        [JsonIgnore]
        public HardwareInfo? Hardware { get; set; }
        public ICollection<MaintenanceHistory> HistoreRecords { get; set; } = new List<MaintenanceHistory>();

    }
}
