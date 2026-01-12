using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResearcherApiPrototype_1.Models
{
    public class ServiceRequest
    {
        [Key]
        public int Id { get; set; }
        public string Discription { get; set; }
        public string CurrentStatus { get; set; } // new, accepted, passed, Done
        public string ServiceRequestType { get; set; } // incident, planned, single
        public bool IsFailure { get; set; }
        public string Creator { get; set; }
        public string Implementer { get; set; }

        public DateTime CreatetAt { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime ClosedAt { get; set; }
        [ForeignKey("HardwareInfo")]
        public int HardwareId { get; set; }
        [JsonIgnore]
        public HardwareInfo Hardware { get; set; }
    }

    public enum ServiceRequestStatus
    {
        New = 0,
        Accepted,
        Passed,
        Done
    }  
}
