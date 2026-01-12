namespace ResearcherApiPrototype_1.Models
{
    public class FileModel
    {
        public  int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public byte[] FileData { get; set; }

    }
}
