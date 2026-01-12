using ResearcherApiPrototype_1.Models;
using System.Reflection.PortableExecutable;

namespace ResearcherApiPrototype_1.DTOs.CharacteristicsDTOs
{
    public class CharMassCreateDTO
    {
        public int HardwareId { get; set; }
        public List<CharCreateDTO> characteristics { get; set; }
    }
}
