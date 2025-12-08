using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Repos.DocumentRepo;
using ResearcherApiPrototype_1.Repos.FileStorageRepo;

namespace ResearcherApiPrototype_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileStorageController : ControllerBase
    {
        private readonly IFileStorageRepo _fileStorageRepo;
        private readonly IDocumentRepo _documentsRepo;

        public FileStorageController(IFileStorageRepo fileStorageRepo, IDocumentRepo repo)
        {
            _fileStorageRepo = fileStorageRepo;
            _documentsRepo = repo;
        }

        [HttpPost("images/upload")]
        public async Task<ActionResult<FileResponseDTO>> UploadFile([FromForm] FileUploadDTO dto)
        {
            var res = await _fileStorageRepo.UploadFileAsync(dto);
            return Ok(res);
        }

        [HttpPost("documents/upload")]
        public async Task<ActionResult<DocumentResponseDTO>> UploadDocument([FromForm] DocumentUploadDTO dto)
        {
            var res = await _documentsRepo.UploadDoc(dto);
            return Ok(res);
        }

        [HttpGet("images/download")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var file = await _fileStorageRepo.DownloadFileAsync(id);
            return File(file.FileData, file.ContentType, file.FileName);
        }

        [HttpGet("image/fileInfo")]
        public async Task<ActionResult<FileResponseDTO>> GetFileInfo(int id)
        {
            var fileInfo = await _fileStorageRepo.GetFileInfoAsync(id);
            return Ok(fileInfo);
        }
             
    }
}
