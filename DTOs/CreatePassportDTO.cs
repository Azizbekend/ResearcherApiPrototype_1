namespace ResearcherApiPrototype_1.DTOs
{
    public class CreatePassportDTO
    {
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Adress { get; set; }
        public string OperatingOrganization { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string GeneralContractorName { get; set; } = string.Empty;
        public double ProjectEfficiency { get; set; }
    }
}
