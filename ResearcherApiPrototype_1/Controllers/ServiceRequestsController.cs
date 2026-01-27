using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using Opc.Ua;
using ResearcherApiPrototype_1.DTOs.BaseCreateDTOs;
using ResearcherApiPrototype_1.DTOs.CommonServisesDTOs;
using ResearcherApiPrototype_1.Models;
using ResearcherApiPrototype_1.Models.ServiceRequests;
using ResearcherApiPrototype_1.Repos.ServiceRequestRepo;
using System.Runtime.InteropServices;

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
        [HttpGet("services/all")]
        public async Task<ActionResult<ICollection<CommonServiceRequest>>> GetAllRequests()
        {
            try
            {
                var list = await _serviceRepo.GetAllServiceRequestsAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("commonServices/stage/last")]
        public async Task <IActionResult> GetLastStage(int requestId)
        {
            try
            {
                var res = _serviceRepo.GetLastServiceStage(requestId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("object/services/all")]
        public async Task<ActionResult<ICollection<CommonServiceRequest>>> GetObjectsRequests(BaseDTO dto)
        {
            try
            {
                var list = await _serviceRepo.GetAllObjectRequests(dto.Id);
                return Ok(list);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("services/incidentServices/all")]
        public async Task<IActionResult> GetIncidentRequests(int id)
        {
            try
            {
                var res = await _serviceRepo.GetAllIncidentRequestsByIncId(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("services/stages/user/all")]
        public async Task<ActionResult<ICollection<CommonRequestStage>>> GetUserStages(int id)
        {
            try
            {
                var stages = await _serviceRepo.GetAllUsersStages(id);
                return Ok(stages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("stage/services/all")]
        public async Task<ActionResult<ICollection<CommonServiceRequest>>> GetRequestStages(BaseDTO dto)
        {
            try
            {
                var list = await _serviceRepo.GetRequestStagesAsync(dto.Id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("mainEngineer/commonService/InitialCreate")]
        public async Task<IActionResult> InitialCreateCommonRequest(CreateRequestME_DTO dto)
        {
            try 
            {
                var request = await _serviceRepo.CreateServiceRequest(dto);
                var stage = new CreateStageME_DTO
                {
                    CreatorId = dto.CreatorId,
                    CreatorsCompanyId = dto.CreatorsCompanyId,
                    ImplementerId = dto.ImplementerId,
                    ImplementersCompanyId = dto.ImplementersCompaneId,
                    ServiceId = request.Id,
                    Discription = dto.Discription,
                    StageType = "InitialCommon"
                };
                await _serviceRepo.CreateInitialRequestStage(stage);
                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPost("mainEngineer/incidentService/InitialCreate")]
        public async Task<IActionResult> InitialCreateIncidentRequest(CreateIncidentServiceRequestDTO dto)
        {
            try
            {
                var request = await _serviceRepo.CreateIncidentServiceRequest(dto);
                var stage = new CreateStageME_DTO
                {
                    CreatorId = dto.CreatorId,
                    CreatorsCompanyId= dto.CreatorsCompanyId,
                    ImplementerId = dto.ImplementerId,
                    ImplementersCompanyId = dto.ImplementersCompaneId,
                    ServiceId = request.Id,
                    Discription = dto.Discription,
                    StageType = "InitialIncident"
                };
                var req = await _serviceRepo.CreateInitialRequestStage(stage);
                await _serviceRepo.CreateIncidentLink(request.Id, dto.IncidentId);
                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        //[HttpPost("mainEngineer/commonService/create")]
        //public async Task<IActionResult> CreateCommonService(CreateRequestME_DTO dto)
        //{
        //    try
        //    {
        //        var req = await _serviceRepo.CreateServiceRequest(dto);
        //        return Ok(req);
        //    }
        //    catch (Exception ex) 
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("mainEngineer/incidentService/create")]
        //public async Task<IActionResult> CreateIncidentService(CreateIncidentServiceRequestDTO dto)
        //{
        //    var req = await _serviceRepo.CreateIncidentServiceRequest(dto);
        //    await _serviceRepo.CreateIncidentLink(req.Id, dto.IncidentId);
        //    return Ok(req);
        //}

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
            try
            {
                await _serviceRepo.CancelRequest(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost("mainEngineer/serviceStage/create")]
        public async Task<IActionResult> CreateServiseStage(CreateStageME_DTO dto)
        {
            try
            {
                var stage = await _serviceRepo.CreateRequestStage(dto);
                return Ok(stage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("common/serviceStage/complete")]
        public async Task<IActionResult> CompleteStageCommon(CompleteStageDTO dto)
        {
            try
            {
                await _serviceRepo.CompleteStage(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("mainIngineer/serviceStage/complete")]
        public async Task<IActionResult> CompleteStageME(CompleteStageME_DTO dto)
        {
            try
            {
                var res = await _serviceRepo.CompleteStageME(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("mainEngineer/serviceStage/Cancel")]
        public async Task<IActionResult> CancelStageME(CancelStageME_DTO dto)
        {
            try
            {
                await _serviceRepo.CancelStageME(dto);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}

