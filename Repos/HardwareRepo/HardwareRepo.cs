using ResearcherApiPrototype_1.Models;
using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1.DTOs;

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
                DeveloperName = dto.DeveloperName,
                SupplierName = dto.SupplierName,
                Position = dto.Position,
                PhotoName = dto.PhotoName,
                ControlBlockId = dto.ControlBlockId
            };
            _appDbContext.Hardwares.Add(hardware);
            await _appDbContext.SaveChangesAsync();

            return hardware;
        }

        public async Task<ICollection<HardwareInfo>> GetAllHardwaresAsync()
        {
            return await _appDbContext.Hardwares
            .Include(h => h.ControlBlock)
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
                .ToListAsync();
        }
    }
}
