using System.ComponentModel.DataAnnotations.Schema;

namespace ResearcherApiPrototype_1.DTOs
{
    public class ServiceRequestCreateDTO
    {
        public string Discription { get; set; }
        public string CurrentStatus { get; set; }
        public bool IsFailure { get; set; }
        public string Creator { get; set; }
        public string Implementer { get; set; }
        public int HardwareId { get; set; }
    }
}
