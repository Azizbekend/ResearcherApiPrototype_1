using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.ServiceRequestRepo
{
    public class ServiceRepo : IServiceRepo
    {
        private readonly AppDbContext _context;

        public ServiceRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task AttachImplementer(int id, string name)
        {
            var request = new ServiceRequest
            {
                Id = id,
                Implementer = name
            };
            _context.ServiceRequests.Attach(request);
            await _context.SaveChangesAsync();
        }

        public async Task CloseRequest(int id)
        {
            var request = new ServiceRequest
            {
                Id = id,
                CurrentStatus = "Completed"
            };
            _context.ServiceRequests.Attach(request);
            await _context.SaveChangesAsync();
        }

        public async Task<ServiceRequest> Create(ServiceRequestCreateDTO dto)
        {
            var newRequest = new ServiceRequest()
            {
                Discription = dto.Discription,
                CurrentStatus = dto.CurrentStatus,
                IsFailure = dto.IsFailure,
                Creator = dto.Creator,
                Implementer = dto.Implementer,
                HardwareId = dto.HardwareId
            };
            _context.ServiceRequests.Add(newRequest);   
            await _context.SaveChangesAsync();
            return newRequest;
        }

        public async Task StatusChange(int id, string status)
        {
            var request = new ServiceRequest()
            {
                Id = id,
                CurrentStatus = status
            };
            _context.ServiceRequests.Attach(request);
            await _context.SaveChangesAsync();
        }
    }
}
