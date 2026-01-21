namespace ResearcherApiPrototype_1.DTOs.CommonServisesDTOs
{
    public class CreateIncidentServiceRequestDTO
    {
        public string Title { get; set; }
        public string Discription { get; set; }
        public int IncidentId { get; set; }
        public int CreatorId { get; set; }
        public int CreatorsCompanyId { get; set; }
        public int ImplementerId { get; set; }
        public int ImplementersCompaneId { get; set; }
        public int HardwareId { get; set; }
        public int ObjectId { get; set; }
    }
}
