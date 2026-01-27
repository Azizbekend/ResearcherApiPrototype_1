namespace ResearcherApiPrototype_1.DTOs.FilesAndDocsDTOs
{
    public class ObjDocumentUploadDTO
    {
        public int ObjectId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public IFormFile File { get; set; }
    }
}
