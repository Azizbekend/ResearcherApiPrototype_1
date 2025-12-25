using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.ControlBlockRepo
{
    public interface IControlBlockRepo
    {
        Task<ControlBlockInfo> CreateControlBlock(string name, string IpAdress, int passportId);
        Task<ICollection<ControlBlockInfo>> GetAllControlBlocks();
        Task<ControlBlockInfo> GetByHardwareId(int  hardwareId) ;
        Task<ICollection<ControlBlockInfo>> GetControlBlockInfoByPassportId(int passportId);
        Task<ControlBlockInfo> GetControlBlockInfoByName(string name);
    }
}
