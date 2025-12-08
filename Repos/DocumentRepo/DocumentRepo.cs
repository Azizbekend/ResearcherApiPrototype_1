using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.DocumentRepo
{
    public class DocumentRepo : IDocumentRepo
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public DocumentRepo(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }
        public async Task<DocumentResponseDTO> UploadDoc(DocumentUploadDTO documentUploadDTO)
        {
            using var memoryStream = new MemoryStream();
            await documentUploadDTO.File.CopyToAsync(memoryStream);

            var document = new DocumentModel
            {
                Title = documentUploadDTO.Title,
                HardwareId = documentUploadDTO.HardwareId,
                FileName = documentUploadDTO.File.FileName,
                ContentType = documentUploadDTO.File.ContentType,
                FileSize = documentUploadDTO.File.Length,
                FileData = memoryStream.ToArray()
            };
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
            return CreateFileResponse(document);
        }

        private DocumentResponseDTO CreateFileResponse(DocumentModel fileMdel)
        {
            var request = _contextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";

            return new DocumentResponseDTO
            {
                Id = fileMdel.Id,
                FileName = fileMdel.FileName,
                ContentType = fileMdel.ContentType,
                FileSize = fileMdel.FileSize,
                DownloadUrl = $"{baseUrl}/api/files/download/{fileMdel.Id}"
            };

        }
    }
}
