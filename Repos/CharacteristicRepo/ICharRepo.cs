using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.CharacteristicRepo
{
    public interface ICharRepo
    {
        Task<HardwareCharacteristic> Create(CharCreateDTO characteristic);
        Task CreateMass(CharMassCreateDTO dto);
        Task<ICollection<HardwareCharacteristic>> FindByHardwareId(int id);
        Task<HardwareCharacteristic> UpdateInfo(CharUpdateDTO characteristic);
        Task Delete(int id) ;
        

    }
}
