using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1.DTOs.BaseCreateDTOs;
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
                 HardwareId = dto.HardwareId
            };
            _context.ServiceRequests.Add(newRequest);   
            await _context.SaveChangesAsync();
            return newRequest;
        }

        public async Task<ICollection<ServiceRequest>> GetAllObjectServiceRequests(int id)
        {
            var blocks = await _context.ControlBlocks.Where(x => x.StaticObjectInfoId == id).ToListAsync();
            var list = new List<ServiceRequest>();
            foreach (var block in blocks)
            {
                var hardwares = await _context.Hardwares.Where(x=> x.ControlBlockId == block.Id).ToListAsync();
                foreach (var item in hardwares)
                {
                    list.AddRange(await _context.ServiceRequests.Where(x => x.HardwareId == item.Id).ToListAsync());
                }
            }
            return list;
        }

        public async Task<ICollection<ServiceRequest>> GetAllRequests()
        {
            return await _context.ServiceRequests.ToListAsync();
        }

        
        public async Task<ICollection<ServiceRequest>> GetHardwareRequests(int id)
        {
            return await _context.ServiceRequests
                .Where(x => x.HardwareId == id)
                .ToListAsync();
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
