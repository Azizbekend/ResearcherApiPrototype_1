using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs.FilesAndDocsDTOs;
using ResearcherApiPrototype_1.Models;
using ResearcherApiPrototype_1.Repos.DocumentRepo;
using ResearcherApiPrototype_1.Repos.FileStorageRepo;
using ResearcherApiPrototype_1.Repos.ObjectPassportRepo;

namespace ResearcherApiPrototype_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileStorageController : ControllerBase
    {
        private readonly IFileStorageRepo _fileStorageRepo;
        private readonly IDocumentRepo _documentsRepo;
        private readonly IObjectPassportRepo _objectPassportRepo;

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
        [HttpGet("object/documents/all")]
        public async Task<IActionResult> GetobjectpAssportDocs(int id)
        {
            var res = await _objectPassportRepo.GetObjectDocLinks(id);
            return Ok(res);
        }
        [HttpPost("object/document/upload")]
        public async Task<ActionResult<ICollection<ObjectDocLink>>> UploadObjDoc([FromForm] ObjDocumentUploadDTO dto)
        {
            var file = new FileUploadDTO()
            {
                File = dto.File
            };
            var res = await _fileStorageRepo.UploadFileAsync(file);
            await _fileStorageRepo.UploadObjectDocAsync(dto.ObjectId, res.Id, dto.DocumentName, dto.DocumentType);
            return Ok(res);
        }
        [HttpDelete("object/document/id")]
        public async Task<IActionResult> DeleteObjDoc(int id)
        { 
            await _objectPassportRepo.DeleteObjectDoc(id);
            return Ok();
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
