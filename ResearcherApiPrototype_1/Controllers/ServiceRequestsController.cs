using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using ResearcherApiPrototype_1.DTOs.BaseCreateDTOs;
using ResearcherApiPrototype_1.Models;
using ResearcherApiPrototype_1.Repos.ServiceRequestRepo;

namespace ResearcherApiPrototype_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly IServiceRepo _serviceRepo;

        public ServiceRequestsController(IServiceRepo serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ServiceRequestCreateDTO>> CreateRequest(ServiceRequestCreateDTO serviceRequestCreateDTO)
        {
            var sr = await _serviceRepo.Create(serviceRequestCreateDTO);
            return Ok(sr);
        }
        [HttpGet("hardware/all")]
        public async Task<ActionResult<ICollection<ServiceRequest>>> GetHardwareRequestsAll(int id)
        {
            var sr = await _serviceRepo.GetHardwareRequests(id);
            return Ok(sr);
        }
        [HttpGet("all")]
        public async Task<ActionResult<ICollection<ServiceRequest>>> GetAll()
        {
            var sr =  await _serviceRepo.GetAllRequests();
            return Ok(sr);
        }
        [HttpGet("object/all")]
        public async Task<ActionResult<ICollection<ServiceRequest>>> GetAllByObjectId(int id)
        {
            var sr = await _serviceRepo.GetAllObjectServiceRequests(id);
            return Ok(sr);
        }

    }
}
