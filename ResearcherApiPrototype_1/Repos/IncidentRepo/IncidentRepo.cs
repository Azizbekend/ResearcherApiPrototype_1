using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1.DTOs.IncidentDTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.IncidentRepo
{
    public class IncidentRepo : IIncidentRepo
    {
        private readonly AppDbContext _context;

        public IncidentRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Incident>> GetAllIncidents()
        {
            return await _context.Incidents.ToListAsync();
        }

        public async Task<ICollection<Incident>> GetIncidentsByObjectId(int objectId)
        {
            return await _context.Incidents.Where(x=> x.ObjectId == objectId).ToListAsync();
        }

        public async Task<ICollection<CommonIncidentTableGetDTO>> GetIncidentsByObjectIdForTable(int objectId)
        {
            var list = new List<CommonIncidentTableGetDTO>();
            var staticObject = await _context.StaticObjectInfos.FirstOrDefaultAsync(x => x.Id == objectId);

            var incidents = await _context.Incidents.Where(x => x.ObjectId == objectId).ToListAsync();
            foreach(var inc in incidents)
            {
                var hardware = await _context.Hardwares.FirstOrDefaultAsync(x=> x.Id == inc.HardwareId);
                var dto = new CommonIncidentTableGetDTO()
                {
                    ObjectId = staticObject.Id,
                    ObjectName = staticObject.Name,
                    HardwareId = hardware.Id,
                    HardwareName = hardware.Name,
                    CreatedAt = inc.CreatedAt,
                    ClosedAt = inc.ClosedAt,
                };
                list.Add(dto);
            }
            return list;
        }
    }
}
