using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.ObjectPassportRepo
{
    public interface IObjectPassportRepo
    {
        Task<StaticObjectInfo> Create(StaticObjectInfo dto);
        Task<ICollection<StaticObjectInfo>> GetAll();
    }
}
