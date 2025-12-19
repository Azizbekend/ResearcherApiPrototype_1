namespace ResearcherApiPrototype_1.DTOs.IncidentDTOs
{
    public class CommonIncidentTableGetDTO
    {
        public int ObjectId { get; set; }
        public string ObjectName { get; set; }
        public int HardwareId { get; set; }
        public string HardwareName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ClosedAt { get; set; }
        public string ServiceManName { get; set; } = "Не назначен";
        public string ServiceCompanyName { get; set; } = "Не назначен";
    }
}
