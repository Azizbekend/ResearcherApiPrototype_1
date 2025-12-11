using ResearcherApiPrototype_1.Models;
using Microsoft.EntityFrameworkCore;

namespace ResearcherApiPrototype_1.Repos.ControlBlockRepo
{
    public class ControlBlockRepo : IControlBlockRepo
    {
        private readonly AppDbContext _appDbContext;

        public ControlBlockRepo(AppDbContext context)
        {
            _appDbContext = context;
        }
        public async Task<ControlBlockInfo> CreateControlBlock(string name, string IpAdress, int passportId)
        {
            var controlBlock = new ControlBlockInfo { Name = name, PlcIpAdress = IpAdress, StaticObjectInfoId = passportId };
            _appDbContext.ControlBlocks.Add(controlBlock);
            await _appDbContext.SaveChangesAsync();
            return controlBlock;
        }

        public async Task<ICollection<ControlBlockInfo>> GetAllControlBlocks()
        {
            return await _appDbContext.ControlBlocks
                .ToListAsync();
        }

        public async Task<ICollection<ControlBlockInfo>> GetControlBlockInfoByPassportId(int passportId)
        {
            return await _appDbContext.ControlBlocks
                .Include(p => p.StaticObjectInfoId)
                .ToListAsync();

        }

        public async Task<ControlBlockInfo> GetControlBlockInfoByName(string name)
        {
            return await _appDbContext.ControlBlocks
                .FirstOrDefaultAsync(c => c.Name == name);

        }
    }
}
