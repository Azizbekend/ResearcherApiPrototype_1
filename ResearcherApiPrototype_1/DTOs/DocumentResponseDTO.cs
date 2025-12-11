using ResearcherApiPrototype_1.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearcherApiPrototype_1.DTOs
{
    public class DocumentResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public string DownloadUrl { get; set; }
    }
}
