using ResearcherApiPrototype_1.DTOs.BaseCreateDTOs;
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
        Task<HardwareStatusDTO> GetHardwaresStatusByIdAsync(BaseSendListDTO dto);
        Task<ICollection<HardwareIncidentGroupDTO>> HadrdwareStatusCheck(BaseSendListDTO dto);
        Task<ICollection<NodeInfo>> HardwareIncidentsCheck(int id);
        Task HardwareActivating(int id);
        Task<HardwareInfo> HardwareInfoUpdate(HardwareInfoUpdateDTO dto);
        Task HardwareDelete(int id);

    }
}
