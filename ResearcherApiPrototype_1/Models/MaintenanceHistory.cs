using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResearcherApiPrototype_1.Models
{
    public class MaintenanceHistory
    {
        [Key]
        public int Id { get; set; }
        public DateTime CompletedMaintenanceDate { get; set; }
        public DateTime SheduleMaintenanceDate { get; set; }
        [ForeignKey("MaintenanceRequest")]
        public int MaintenanceRequestId { get; set; }
        [JsonIgnore]
        public MaintenanceRequest MaintenanceRequest { get; set; }
    }
}
