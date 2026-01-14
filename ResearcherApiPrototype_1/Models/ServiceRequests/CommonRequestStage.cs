using System.ComponentModel.DataAnnotations;

namespace ResearcherApiPrototype_1.Models.ServiceRequests
{
    public class CommonRequestStage
    {
        [Key]
        public int Id { get; set; }
        public string Discription { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ClosedAt { get; set; }
        public string ClosingDiscription { get; set; }
        public int CreatorId { get; set; }
        public int ImplementerId { get; set; }
        public int PassedToId { get; set; } //кому передано

    }
}
