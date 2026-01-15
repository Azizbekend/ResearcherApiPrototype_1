namespace ResearcherApiPrototype_1.DTOs.CommonServisesDTOs
{
    public class SupplyRequestInitialCreateDTO
    {
        public int CreatorId { get; set; }
        public int CurrentImplementerId { get; set; }
        public string ProductName { get; set; }
        public double RequiredCount { get; set; }
        public int HardwareId { get; set; }
        public int ObjectId { get; set; }
    }
}
