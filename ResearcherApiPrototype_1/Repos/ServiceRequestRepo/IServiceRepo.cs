using ResearcherApiPrototype_1.DTOs.BaseCreateDTOs;
using ResearcherApiPrototype_1.DTOs.CommonServisesDTOs;
using ResearcherApiPrototype_1.Models;
using ResearcherApiPrototype_1.Models.ServiceRequests;

namespace ResearcherApiPrototype_1.Repos.ServiceRequestRepo
{
    public interface IServiceRepo
    {
        Task<CommonServiceRequest> CreateServiceRequest(CreateRequestME_DTO dto);
        Task <CommonRequestStage> CreateRequestStage(CreateStageME_DTO dto);
    }
}
