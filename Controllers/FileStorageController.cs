using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Repos.FileStorageRepo;

namespace ResearcherApiPrototype_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileStorageController : ControllerBase
    {
        private readonly IFileStorageRepo _fileStorageRepo;

        public FileStorageController(IFileStorageRepo fileStorageRepo)
        {
            _fileStorageRepo = fileStorageRepo;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<FileResponseDTO>> UploadFile([FromForm] FileUploadDTO dto)
        {
            var res = await _fileStorageRepo.UploadFileAsync(dto);
            return Ok(res);
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var file = await _fileStorageRepo.DownloadFileAsync(id);
            return File(file.FileData, file.ContentType, file.FileName);
        }

        [HttpGet("fileInfo")]
        public async Task<ActionResult<FileResponseDTO>> GetFileInfo(int id)
        {
            var fileInfo = await _fileStorageRepo.GetFileInfoAsync(id);
            return Ok(fileInfo);
        }
             
    }
}
