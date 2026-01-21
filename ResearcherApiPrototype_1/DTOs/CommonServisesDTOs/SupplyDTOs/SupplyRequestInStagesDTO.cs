namespace ResearcherApiPrototype_1.DTOs.CommonServisesDTOs.SupplyDTOs
{
    public class SupplyRequestInStagesDTO
    {
        public int CreatorId { get; set; }
        public int CurrentImplementerId { get; set; }
        public int CurrentImplementerCompanyId { get; set; }
        public string ProductName { get; set; }
        public double RequiredCount { get; set; }
        public int HardwareId { get; set; }
        public int ObjectId { get; set; }
        public int ServiceId { get; set; }
    }
}
