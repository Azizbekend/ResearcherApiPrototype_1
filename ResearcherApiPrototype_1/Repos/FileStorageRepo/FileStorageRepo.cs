using ResearcherApiPrototype_1.DTOs.FilesAndDocsDTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.FileStorageRepo
{
    public class FileStorageRepo : IFileStorageRepo
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public FileStorageRepo(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<FileModel> DownloadFileAsync(int id)
        {
            var file = await _context.Files.FindAsync(id);
            if (file == null)
                throw new FileNotFoundException("File doesn't exists!");

            return file;
        }

        public async Task<FileResponseDTO> GetFileInfoAsync(int id)
        {
            var file = await _context.Files.FindAsync(id);
            if (file == null)
                throw new FileNotFoundException($"{nameof(File)} does not exist");

            return CreateFileResponse(file);

        }

        public async Task<FileResponseDTO> UpdateFileAsync(FileUpdateDTO file)
        {
            using var memoryStream = new MemoryStream();
            await file.File.CopyToAsync(memoryStream);

            var fileModel = new FileModel
            {
                Id = file.Id,
                FileName = file.File.FileName,
                ContentType = file.File.ContentType,
                FileSize = file.File.Length,
                FileData = memoryStream.ToArray()
            };
            _context.Files.Attach(fileModel);
            _context.Entry(fileModel).Property(x => x.FileData).IsModified = true;
            await _context.SaveChangesAsync();
            return CreateFileResponse(fileModel);
        }



        public async Task<FileResponseDTO> UploadFileAsync(FileUploadDTO dto)
        {
            using var memoryStream = new MemoryStream();
            await dto.File.CopyToAsync(memoryStream);

            var fileModel = new FileModel
            {
                FileName = dto.File.FileName,   
                ContentType = dto.File.ContentType,
                FileSize = dto.File.Length,
                FileData = memoryStream.ToArray()
            };
            _context.Files.Add(fileModel);
            await _context.SaveChangesAsync();
            return CreateFileResponse(fileModel);
        }

        public async Task UploadObjectDocAsync(int objId, int fileId)
        {     
            var docLink = new ObjectDocLink()
            {
                DocId = objId,
                ObjectId = fileId                
            };
            _context.ObjectDocs.Add(docLink);
            await _context.SaveChangesAsync();

        }

        private FileResponseDTO CreateFileResponse(FileModel fileMdel)
        {
            var request = _contextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";

            return new FileResponseDTO
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
