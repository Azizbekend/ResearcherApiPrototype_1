using ResearcherApiPrototype_1.DTOs.FilesAndDocsDTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.DocumentRepo
{
    public interface IDocumentRepo
    {
        Task<DocumentResponseDTO> UploadDoc(DocumentUploadDTO documentUploadDTO);
        Task<DocumentModel> DownloadDoc (DocumentDownloadDTO documentDownloadDTO);
        Task<ICollection<DocumentResponseDTO>> GetHardwareDocs(int id);

    }
}
