namespace ResearcherApiPrototype_1.DTOs.FilesAndDocsDTOs
{
    public class ObjDocumentUploadDTO
    {
        public int DocId { get; set; }
        public int ObjectId { get; set; }
        public IFormFile File { get; set; }
    }
}
