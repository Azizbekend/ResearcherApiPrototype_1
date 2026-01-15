using System.ComponentModel.DataAnnotations;

namespace ResearcherApiPrototype_1.Models.ServiceRequests
{
    public class SupplyRequest
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double RequiredCount { get; set; }
        public string SupplierName { get; set; }
        public double RealCount { get; set; }
        public string ExpenseNumber { get; set; }
        public double Expenses { get; set; } //расходы
        public bool IsPayed { get; set; }
        public int CreatorId { get; set; }
        public int CurrentImplementerId { get; set; }
        public string CurrentStatus { get; set; } // New, Accepted, Passed, Done
        public int CommonRequestId { get; set; }
    }
}
