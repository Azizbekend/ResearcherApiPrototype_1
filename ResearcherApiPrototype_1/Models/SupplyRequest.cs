using System.ComponentModel.DataAnnotations;

namespace ResearcherApiPrototype_1.Models
{
    public class SupplyRequest
    {
        [Key]
        public int Id { get; set; }
        public string Discription { get; set; }
        public string Creator { get; set; }
        public string Implementer { get; set; }
        public string CurrentStatus { get; set; } // New, Accepted, Passed, Done
        public double Expenses { get; set; } //расходы
        public int ServiceRequestId { get; set; }
    }
}
