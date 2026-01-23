using ResearcherApiPrototype_1.DTOs.BaseCreateDTOs;
using ResearcherApiPrototype_1.DTOs.CommonServisesDTOs;
using ResearcherApiPrototype_1.DTOs.CommonServisesDTOs.SupplyDTOs;
using ResearcherApiPrototype_1.Models;
using ResearcherApiPrototype_1.Models.ServiceRequests;

namespace ResearcherApiPrototype_1.Repos.ServiceRequestRepo
{
    public interface IServiceRepo
    {
        Task<CommonServiceRequest> CreateServiceRequest(CreateRequestME_DTO dto);
        Task <CommonRequestStage> CreateRequestStage(CreateStageME_DTO dto);
        Task<CommonRequestStage> CreateSupplyRequestStage(CreateStageME_DTO dto);
        Task<CommonRequestStage> CreateInitialRequestStage(CreateStageME_DTO dto);
        Task<CommonServiceRequest> CreateIncidentServiceRequest(CreateIncidentServiceRequestDTO dto);
        Task CreateIncidentLink(int requestId, int incidentId);
        Task<SupplyRequest> CreateSupplyRequest(SupplyRequestInitialCreateDTO dto, int serviceId);
        Task<bool> IsServiceRequestExists(int id);
        Task CreateSupplyServiceLink(int serviceId, int supplyId);
        Task<ICollection<CommonRequestStage>> GetRequestStagesAsync(int id);

        Task<ICollection<CommonServiceRequest>> GetAllServiceRequestsAsync();
        Task<ICollection<CommonServiceRequest>> GetAllObjectRequests(int id);
        Task<ICollection<CommonServiceRequest>> GetAllIncidentRequestsByIncId(int id);
        Task<ICollection<CommonRequestStage>> GetAllUsersStages(int id);
        Task <CommonRequestStage> GetLastServiceStage(int id) ;
        Task SupplyRequestWarehouseConfirm(SupplyRequestConfirmWarehouseDTO dto);
        Task SupplyRequestAttachExpUpdate(SupplyRequestAttachExpenseDTO dto);
        Task SupplyRequestAttachPay(SupplyRequestAttachPay dto);
        Task SupplyRequestConfirmWarehouseSupply(SupplyWarehouseConfirmDTO dto);
        Task CompleteRequest(CompleteCancelRequestME_DTO dto);
        Task ConfirmSupplyStage(CompleteSupplyStageDTO dto);
        Task CancelSupplyStage(CancelSupplyStageDTO dto);

        Task CancelRequest(CompleteCancelRequestME_DTO dto);
        Task CompleteStage(CompleteStageDTO dto);
        Task<CommonRequestStage> CompleteStageME(CompleteStageME_DTO dto);
        Task CancelStageME(CancelStageME_DTO dto);
        Task DeleteSupplyRequest(int id);

    }
}
