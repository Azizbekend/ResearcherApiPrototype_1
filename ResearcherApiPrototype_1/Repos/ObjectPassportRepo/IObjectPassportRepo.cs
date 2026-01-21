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
        Task<ICollection<StaticObjectInfo>> GetUserObjects(int id);
        Task<AttachCompanyToObjectDTO> AttachCompany(AttachCompanyToObjectDTO dto);
        Task<ObjectCompanyLink> GetCopmanyObjectLint(int companyId, int objId);
        Task<AttachUserToObjectLinkDTO> AttachUser(AttachUserToObjectLinkDTO dto);
        Task<UserObjectCompanyLink> GetUserCompanyLink(int userId, int objCompLinkId);
        Task<ICollection<UserObjectCompanyLink>> GetAzizbeckLink(int objCompLink);
        Task CreateAccesses(CreateObjectAccessesDTO dto); 
        Task<UpdateObjAccessesDTO> UpdateUsersAccesses(UpdateObjAccessesDTO dto);
        Task DeleteAccesses(int userId, int userCompLinkId);
    }
}
