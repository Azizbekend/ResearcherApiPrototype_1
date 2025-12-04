using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.FileStorageRepo
{
    public interface IFileStorageRepo
    {
        Task<FileResponseDTO> UploadFileAsync(FileUploadDTO file);
        Task<FileModel> DownloadFileAsync(int id);
        Task <FileResponseDTO> GetFileInfoAsync(int id);
    }
}
