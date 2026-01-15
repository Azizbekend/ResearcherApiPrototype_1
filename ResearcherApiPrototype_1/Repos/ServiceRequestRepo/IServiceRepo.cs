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
        Task<CommonRequestStage> CreateInitialRequestStage(CreateStageME_DTO dto);
        Task<CommonServiceRequest> CreateIncidentServiceRequest(CreateIncidentServiceRequestDTO dto);
        Task CreateIncidentLink(int requestId, int incidentId);
        Task<ICollection<CommonRequestStage>> GetRequestStagesAsync(int id);
        Task<ICollection<CommonServiceRequest>> GetAllServiceRequestsAsync();
        Task<ICollection<CommonServiceRequest>> GetAllObjectRequests(int id);
        Task<ICollection<CommonRequestStage>> GetAllUsersStages(int id);
        Task CompleteRequest(CompleteCancelRequestME_DTO dto);
        Task CancelRequest(CompleteCancelRequestME_DTO dto);
        Task CompleteStage(CompleteStageDTO dto);
        Task CompleteStageME(CompleteStageME_DTO dto);
        Task CancelStageME(CancelStageME_DTO dto);

    }
}
