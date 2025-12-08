namespace ResearcherApiPrototype_1.DTOs
{
    public class DocumentUploadDTO
    {
        public string Title { get; set; }
        public int HardwareId { get; set; }
        public IFormFile File { get; set; }
    }
}
