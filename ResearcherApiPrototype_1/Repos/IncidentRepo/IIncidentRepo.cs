using ResearcherApiPrototype_1.DTOs.IncidentDTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.IncidentRepo
{
    public interface IIncidentRepo
    {
        Task<ICollection<Incident>> GetAllIncidents();
        Task<ICollection<Incident>> GetIncidentsByObjectId(int  objectId);
        Task<ICollection<CommonIncidentTableGetDTO>> GetIncidentsByObjectIdForTable(int objectId);
    }
}
