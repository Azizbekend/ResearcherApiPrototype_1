using ResearcherApiPrototype_1.Models;
using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1.DTOs.HardwaresDTOs;

namespace ResearcherApiPrototype_1.Repos.HardwareRepo
{
    public class HardwareRepo : IHardwareRepo
    {
        private readonly AppDbContext _appDbContext;

        public HardwareRepo(AppDbContext context)
        {
            _appDbContext = context;
        }
        public async Task<HardwareInfo> CreateHardwareAsync(HardwareCreateDTO dto)
        {
            var hardware = new HardwareInfo
            {
                Name = dto.Name,
                Model = dto.Model,
                Category = dto.Category,
                DeveloperName = dto.DeveloperName,
                SupplierName = dto.SupplierName,
                Position = dto.Position,
                PhotoName = dto.PhotoName,
                OpcDescription = dto.OpcDescription,
                ControlBlockId = dto.ControlBlockId,
                FileId = dto.FileId
            };
            _appDbContext.Hardwares.Add(hardware);
            await _appDbContext.SaveChangesAsync();

            return hardware;
        }

        public async Task<ICollection<HardwareInfo>> GetAllHardwaresAsync()
        {
            return await _appDbContext.Hardwares
            .Include(h => h.ControlBlock).OrderBy(x => x.Id)
            .ToListAsync();
        }

        public async Task<HardwareInfo> GetHardwareByIdAsync(int hardwareId)
        {

            return await _appDbContext.Hardwares
                .Include(h => h.ControlBlock)
                .Where(h => h.Id == hardwareId)
                .FirstAsync();
        }
        public async Task<ICollection<HardwareInfo>> GetHardwaresByControlBlockIdAsync(int controlBlockId)
        {
            
            return await _appDbContext.Hardwares
                .Include (h => h.ControlBlock)
                .Where(h=>h.ControlBlockId == controlBlockId)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task HardwareActivating(int id)
        {
            var hw = new HardwareInfo
            {
                Id = id,
                ActivatedAt = DateTime.Now.ToLocalTime()
            };
            _appDbContext.Hardwares.Attach(hw);
            _appDbContext.Entry(hw).Property(act => act.ActivatedAt).IsModified = true;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task HardwareDelete(int id)
        {
            var hi = await _appDbContext.Hardwares.FirstOrDefaultAsync(h => h.Id == id);
            _appDbContext.Hardwares.Remove(hi);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<HardwareInfo> HardwareInfoUpdate(HardwareInfoUpdateDTO dto)
        {

            var hi = await _appDbContext.Hardwares.FirstOrDefaultAsync(x=>x.Id == dto.Id);
            if (hi != null)
            {
                hi.Name = dto.Name;
                hi.Model = dto.Model;
                hi.Category = dto.Category;
                hi.DeveloperName = dto.DeveloperName;
                hi.SupplierName = dto.SupplierName;
                hi.Position = dto.Position;
                hi.OpcDescription = dto.OpcDescription;
                hi.FileId = dto.FileId;
                _appDbContext.Hardwares.Attach(hi);
                await _appDbContext.SaveChangesAsync();
                return hi;
            }
            else
                throw new Exception("Not Found");

        
        }
    }
}
