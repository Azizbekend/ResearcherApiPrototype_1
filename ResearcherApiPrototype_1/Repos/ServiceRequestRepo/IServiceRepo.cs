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
        Task<ICollection<CommonRequestStage>> GetRequestStages(int id);
        Task CompleteRequest(CompleteCancelRequestME_DTO dto);
        Task CancelRequest(CompleteCancelRequestME_DTO dto);
        Task CompleteStage(CompleteStageDTO dto);
        Task CompleteStageME(CompleteStageME_DTO dto);
        Task CancelStageME(CancelStageME_DTO dto);

    }
}
