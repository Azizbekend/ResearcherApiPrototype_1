using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using Opc.Ua;
using ResearcherApiPrototype_1.DTOs.BaseCreateDTOs;
using ResearcherApiPrototype_1.DTOs.CommonServisesDTOs;
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

        [HttpPost("mainEngineer/commonService/create")]
        public async Task<IActionResult> CreateCommonService(CreateRequestME_DTO dto)
        {
            var req = await _serviceRepo.CreateServiceRequest(dto);
            return Ok(req);
        }

        [HttpPost("mainEngineer/incidentService/create")]
        public async Task<IActionResult> CreateIncidentService(CreateIncidentServiceRequestDTO dto)
        {
            var req = await _serviceRepo.CreateIncidentServiceRequest(dto);
            await _serviceRepo.CreateIncidentLink(req.Id, dto.IncidentId);
            return Ok(req);
        }
        [HttpPost("mainEngineer/commonService/complete")]
        public async Task<IActionResult> CompleteRequestME(CompleteCancelRequestME_DTO dto)
        {
            try
            {
                await _serviceRepo.CompleteRequest(dto);
                return Ok(); 
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("mainEngineer/commonService/Cancel")]
        public async Task<IActionResult> CancelRequestME(CompleteCancelRequestME_DTO dto)
        {
            await _serviceRepo.CancelRequest(dto);
            return Ok();
        }
        [HttpPost("mainEngineer/serviceStage/create")]
        public async Task<IActionResult> CreateServiseStage(CreateStageME_DTO dto)
        {
            var stage = await _serviceRepo.CreateRequestStage(dto);
            return Ok(stage);
        }

        [HttpPost("common/serviceStage/complete")]
        public async Task<IActionResult> CompleteStageCommon(CompleteStageDTO dto)
        {
            await _serviceRepo.CompleteStage(dto);
            return Ok();
        }
        [HttpPost("mainIngineer/serviceStage/complete")]
        public async Task<IActionResult> CompleteStageME(CompleteStageME_DTO dto)
        {
            await _serviceRepo.CompleteStageME(dto);
            return Ok();
        }




    }
}
