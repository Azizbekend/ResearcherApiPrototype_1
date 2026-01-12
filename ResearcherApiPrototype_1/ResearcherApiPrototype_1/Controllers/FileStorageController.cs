using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs.FilesAndDocsDTOs;
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
        [HttpPut("images/update")]
        public async Task<ActionResult<FileResponseDTO>> UploadFile([FromForm] FileUpdateDTO dto)
        {
            var res = await _fileStorageRepo.UpdateFileAsync(dto);
            return Ok(res);
        }
        [HttpPost("documents/upload")]
        public async Task<ActionResult<DocumentResponseDTO>> UploadDocument([FromForm] DocumentUploadDTO dto)
        {
            var res = await _documentsRepo.UploadDoc(dto);
            return Ok(res);
        }

        [HttpGet("document/download")]
        public async Task<IActionResult> DownloadDocument(int id)
        {
            var dto = new DocumentDownloadDTO { Id = id };
            var doc = await _documentsRepo.DownloadDoc(dto);
            return File(doc.FileData, doc.ContentType, doc.FileName);
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

        [HttpGet("documents/hardware")]
        public async Task<ActionResult<ICollection<DocumentResponseDTO>>> GetHardwaresDocs(int id)
        {
            var docs = await _documentsRepo.GetHardwareDocs(id);
            return Ok(docs);
        }
             
    }
}
