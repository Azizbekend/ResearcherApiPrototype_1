using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.MaintenanceRepo
{
    public interface IMaintenanceRepo
    {
        Task<MaintenanceRequest> Create(MaintanceCreateDTO request);
        Task<ICollection<MaintenanceRequest>> GetHardwareMaintenanceRequests(int id);
        Task CompleteMaintenanceRequest(int id);
        Task<ICollection<MaintenanceRequest>> GetNextWeekRequests(int requestId);
        Task<ICollection<MaintenanceHistory>> GetHistoryCompleteRecords(int requestId);
        Task<ICollection<MaintenanceRequest>> GetTodayRequests(int requestId);

    }
}
