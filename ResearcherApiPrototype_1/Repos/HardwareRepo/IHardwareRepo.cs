using ResearcherApiPrototype_1.DTOs.HardwaresDTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.HardwareRepo
{
    public interface IHardwareRepo
    {
        Task<HardwareInfo> CreateHardwareAsync(HardwareCreateDTO dto);
        Task<ICollection<HardwareInfo>> GetAllHardwaresAsync();
        Task<ICollection<HardwareInfo>> GetHardwaresByControlBlockIdAsync(int controlBlockId);
        Task<HardwareInfo> GetHardwareByIdAsync(int id);
        Task<HardwareStatusDTO> GetHardwaresStatusByIdAsync(HardwareStatusCheckDTO dto);
        Task HardwareActivating(int id);
        Task<HardwareInfo> HardwareInfoUpdate(HardwareInfoUpdateDTO dto);
        Task HardwareDelete(int id);

    }
}
