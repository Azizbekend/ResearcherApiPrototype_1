using System.ComponentModel.DataAnnotations;

namespace ResearcherApiPrototype_1.Models
{
    public class CommonServiceRequest
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public int HardwareId { get; set; }

    }
}
