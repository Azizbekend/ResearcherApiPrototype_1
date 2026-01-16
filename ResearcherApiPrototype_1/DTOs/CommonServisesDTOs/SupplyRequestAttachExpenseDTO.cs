namespace ResearcherApiPrototype_1.DTOs.CommonServisesDTOs
{
    public class SupplyRequestAttachExpenseDTO
    {
        public string SupplierName { get; set; }
        public double RealCount { get; set; }
        public string ExpenseNumber { get; set; }
        public double Expenses { get; set; } //расходы
        public bool IsPayed { get; set; }
        public int StageId { get; set; }
        public int CurrentImplementerId { get; set; }
        public int RequestId { get; set; }
        public int SupplyRequestId { get; set; }
    }
}
