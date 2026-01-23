namespace ResearcherApiPrototype_1.DTOs.CommonServisesDTOs.SupplyDTOs
{
    public class SupplyRequestConfirmWarehouseDTO
    {
        public string SupplierName { get; set; }
        public double RealCount { get; set; }
        public int StageId { get; set; }
        public int NextImplementerId { get; set; }
        public int NextImplementerCompanyId { get; set; }
        public int RequestId { get; set; }
    }
}
