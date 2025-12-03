using System.ComponentModel.DataAnnotations.Schema;

namespace ResearcherApiPrototype_1.DTOs
{
    public class MaintanceCreateDTO
    {
        public string Discription { get; set; }
        public int Period { get; set; } //days
        public int HardwareId { get; set; }
    }
}
