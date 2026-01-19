namespace ResearcherApiPrototype_1.DTOs.ObjectDTOs
{
    public class AttachCompanyToObjectDTO
    {
        public int ObjectId { get; set; }
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string CompanyRole { get; set; }
    }
}
