using System.ComponentModel.DataAnnotations;

namespace ResearcherApiPrototype_1.Models
{
    public class Incident
    {
        [Key]
        public int Id { get; set; }
        public string Discription { get; set; }
        public DateTime CreatedAT { get; set; }
        public DateTime ClosedAt { get; set; }
        public int HardwareId { get; set; }
        public int ObjectId { get; set; }
    }
}
