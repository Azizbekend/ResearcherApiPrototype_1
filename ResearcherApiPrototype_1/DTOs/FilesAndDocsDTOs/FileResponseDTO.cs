namespace ResearcherApiPrototype_1.DTOs.FilesAndDocsDTOs
{
    public class FileResponseDTO
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public string DownloadUrl { get; set; }
    }
}
