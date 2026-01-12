using ResearcherApiPrototype_1.DTOs.BaseCreateDTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.ServiceRequestRepo
{
    public interface IServiceRepo
    {
        public Task<ServiceRequest> Create(ServiceRequestCreateDTO dto);
        public Task AttachImplementer(int id, string name);
        public Task StatusChange(int id, string status);
        public Task CloseRequest(int id);
        public Task<ICollection<ServiceRequest>> GetAllRequests();
        public Task<ICollection<ServiceRequest>> GetAllObjectServiceRequests(int id);
        public Task<ICollection<ServiceRequest>> GetHardwareRequests(int id);
    }
}
