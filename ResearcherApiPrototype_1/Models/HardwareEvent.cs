using System.ComponentModel.DataAnnotations;

namespace ResearcherApiPrototype_1.Models
{
    public class HardwareEvent
    {
        [Key]
        public int Id { get; set; }
        public string Discription { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now.ToUniversalTime();
        public int HardwareId { get; set; }
        public int UserId { get; set; }

    }
}
