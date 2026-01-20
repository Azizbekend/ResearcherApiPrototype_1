namespace ResearcherApiPrototype_1.DTOs.HardwaresDTOs
{
    public class CreateCommandEventDTO
    {
        public int NodeInfoId { get; set; }
        public string NodeName { get; set; }
        public string Indicates { get; set; }
        public int HardwareId { get; set; }
        public int UserId { get; set; }
    }
}
