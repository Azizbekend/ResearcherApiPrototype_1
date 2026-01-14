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
        public int CreatorId { get; set; }
        public int ImplementerId { get; set; }
        public int PassedToId { get; set; } //кому передано
        public int? IncidentId { get; set; }
        public int? CommonServiceRequestId { get; set; }
    }
}
