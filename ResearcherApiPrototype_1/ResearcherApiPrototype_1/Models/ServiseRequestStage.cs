using System.ComponentModel.DataAnnotations;

namespace ResearcherApiPrototype_1.Models
{
    public class ServiseRequestStage
    {
        [Key]
        public int Id { get; set; }
        public string Discription { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ClosedAt { get; set; }
        public string Creator { get; set; }
        public string Implementer { get; set; }
        public string PassedTo { get; set; }
        public int? IncidentId { get; set; }
        public int? CommonServiceRequestId { get; set; }
    }
}
