using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.DTOs.ObjectDTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.ObjectPassportRepo
{
    public interface IObjectPassportRepo
    {
        Task<StaticObjectInfo> Create(StaticObjectInfo dto);
        Task<ICollection<StaticObjectInfo>> GetAll();
        Task<StaticObjectInfo> GetSingleById(int id);
        Task<ICollection<ObjectCompanyLink>> GetObjectCompanies(int id);
        Task<ICollection<UserObjectCompanyLink>> GetUsersCompany(int id);
        Task<AttachCompanyToObjectDTO> AttachCompany(AttachCompanyToObjectDTO dto);
        Task<AttachUserToObjectLinkDTO> AttachUser(AttachUserToObjectLinkDTO dto);
        Task CreateAccesses(CreateObjectAccessesDTO dto); 
        Task<UpdateObjAccessesDTO> UpdateUsersAccesses(UpdateObjAccessesDTO dto);
    }
}
