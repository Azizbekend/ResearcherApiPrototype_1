using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.HardwareRepo
{
    public interface IHardwareRepo
    {
        Task<HardwareInfo> CreateHardwareAsync(HardwareCreateDTO dto);
        Task<ICollection<HardwareInfo>> GetAllHardwaresAsync();
        Task<ICollection<HardwareInfo>> GetHardwaresByControlBlockIdAsync(int controlBlockId);
        Task<HardwareInfo> GetHardwareByIdAsync(int id);
    }
}
