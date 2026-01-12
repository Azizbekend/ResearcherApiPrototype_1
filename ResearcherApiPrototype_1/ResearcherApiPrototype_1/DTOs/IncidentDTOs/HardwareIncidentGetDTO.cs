namespace ResearcherApiPrototype_1.DTOs.IncidentDTOs
{
    public class HardwareIncidentGetDTO
   {
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ClosedAt { get; set; }
        public string ServiceManName { get; set; } = "Не назначен";
        public string ServiceCompanyName { get; set; } = "Не назначен";
    }
}
