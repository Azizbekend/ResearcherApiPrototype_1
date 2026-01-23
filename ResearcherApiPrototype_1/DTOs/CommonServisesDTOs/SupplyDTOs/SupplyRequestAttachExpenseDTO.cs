namespace ResearcherApiPrototype_1.DTOs.CommonServisesDTOs.SupplyDTOs
{
    public class SupplyRequestAttachExpenseDTO
    {
        public string SupplierName { get; set; }
        public double RealCount { get; set; }
        public string ExpenseNumber { get; set; }
        public double Expenses { get; set; } //расходы
        public int StageId { get; set; }
        public int NextImplementerId { get; set; }
        public int NextImplementerCompanyId { get; set; }
        public int RequestId { get; set; }
    }
}
