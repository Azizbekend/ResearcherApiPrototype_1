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

        

    }
}
