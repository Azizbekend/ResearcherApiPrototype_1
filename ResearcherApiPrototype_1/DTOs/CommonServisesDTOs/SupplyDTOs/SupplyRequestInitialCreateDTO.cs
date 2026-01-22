namespace ResearcherApiPrototype_1.DTOs.CommonServisesDTOs.SupplyDTOs
{
    public class SupplyRequestInitialCreateDTO
    {
        public int CreatorId { get; set; }
        public int CreatorsCompanyId { get; set; }
        public int NextImplementerId { get; set; }
        public int NextImplementerCompanyId { get; set; }
        public string ProductName { get; set; }
        public double RequiredCount { get; set; }
        public int HardwareId { get; set; }
        public int ObjectId { get; set; }
    }
}
