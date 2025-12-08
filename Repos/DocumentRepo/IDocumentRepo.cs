using ResearcherApiPrototype_1.DTOs;

namespace ResearcherApiPrototype_1.Repos.DocumentRepo
{
    public interface IDocumentRepo
    {
        Task<DocumentResponseDTO> UploadDoc(DocumentUploadDTO documentUploadDTO);

    }
}
