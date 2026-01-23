using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1.DTOs.IncidentDTOs;
using ResearcherApiPrototype_1.Models.ServiceRequests;

namespace ResearcherApiPrototype_1.Repos.IncidentRepo
{
    public class IncidentRepo : IIncidentRepo
    {
        private readonly AppDbContext _context;

        public IncidentRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Incident> CopmpleteIncident(int id)
        {
            var inc = await _context.Incidents.FirstOrDefaultAsync(x => x.Id == id); 
            if (inc == null)
            {
                throw new Exception("Nor found!");
            }
            else
            {
                inc.Status = "Completed";
                _context.Incidents.Attach(inc);
                await _context.SaveChangesAsync();
                return inc;
            }
        }

        public async Task<ICollection<Incident>> GetAllIncidents()
        {
            return await _context.Incidents.ToListAsync();
        }

        public async Task<ICollection<HardwareIncidentGetDTO>> GetHardwareIncidents(int hardwareId)
        {
           var list = new List<HardwareIncidentGetDTO>();
            var incidents = await _context.Incidents.Where(x => x.HardwareId == hardwareId).ToListAsync();
            foreach (var incident in incidents)
            {
                var dto = new HardwareIncidentGetDTO()
                {
                    CreatedAt = incident.CreatedAt,
                    ClosedAt = incident.ClosedAt,
                    Status = incident.Status
                };
                list.Add(dto);
            }
            return list;
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
                    Status = inc.Status,
                    HardwareId = hardware.Id,
                    HardwareName = hardware.Name,
                    CreatedAt = inc.CreatedAt,
                    ClosedAt = inc.ClosedAt,
                };
                list.Add(dto);
            }
            return list;
        }

        public async Task<ICollection<CommonIncidentTableGetDTO>> GetIncidentsForTable()
        {
            var list = new List<CommonIncidentTableGetDTO>();
            var incidents = await _context.Incidents.ToListAsync();
            foreach (var inc in incidents)
            {
                var staticObj = await _context.StaticObjectInfos.FirstOrDefaultAsync(x => x.Id == inc.ObjectId);
                var hardware = await _context.Hardwares.FirstOrDefaultAsync(x => x.Id == inc.HardwareId);
                var dto = new CommonIncidentTableGetDTO()
                {
                    ObjectId = staticObj.Id,
                    ObjectName = staticObj.Name,
                    Status = inc.Status,
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
