namespace ResearcherApiPrototype_1.DTOs
{
    public class HardwareCreateDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string DeveloperName { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public string PhotoName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string OpcDescription { get; set; } = string.Empty;
        public int ControlBlockId { get; set; }
        public int FileId { get; set; }
    }
}
