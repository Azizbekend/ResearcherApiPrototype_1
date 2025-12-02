using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearcherApiPrototype_1.Models
{
    public class ServiceRequest
    {
        [Key]
        public int Id { get; set; }
        public string Discription { get; set; }
        public string CurrentStatus { get; set; }
        public bool IsFailure { get; set; }
        [ForeignKey("HardwareInfo")]
        public int HardwareId { get; set; }

        public DateTime CreatetAt { get; set; }
        public DateTime ClosedAt { get; set; }
        public HardwareInfo Hardware { get; set; }
    }
}
