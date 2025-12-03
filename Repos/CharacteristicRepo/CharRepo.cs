using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.CharacteristicRepo
{
    public class CharRepo : ICharRepo
    {
        private readonly AppDbContext _context;

        public CharRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<HardwareCharacteristic> Create(CharCreateDTO newChar)
        {
            var characteristic = new HardwareCharacteristic()
            {
                HardwareId = newChar.HardwareId,
                Name = newChar.Name,
                Value = newChar.Value
            };
            _context.Characteristics.Add(characteristic);
            await _context.SaveChangesAsync();
            return characteristic;
        }

        public async Task CreateMass(CharMassCreateDTO dto)
        {
            foreach (var item in dto.characteristics)
            {
                var characteristic = new HardwareCharacteristic()
                {
                    HardwareId = dto.HardwareId,
                    Name = item.Name,
                    Value = item.Value
                };            
                _context.Characteristics.Add(characteristic);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<HardwareCharacteristic>> FindByHardwareId(int id)
        {
            return await _context.Characteristics
                .Include(h => h.Hardware)
                .Where(x => x.HardwareId == id)
                .ToListAsync();
        }
    }
}
