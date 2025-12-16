using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs.BaseCreateDTOs;
using ResearcherApiPrototype_1.Models;
using ResearcherApiPrototype_1.Repos.MaintenanceRepo;

namespace ResearcherApiPrototype_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanseShedulerController : ControllerBase
    {
        private readonly IMaintenanceRepo _maintenanceRepo;

        public MaintenanseShedulerController(IMaintenanceRepo maintenanceRepo)
        {
            _maintenanceRepo = maintenanceRepo;
        }
        [HttpGet("next_week")]
        public async Task<ActionResult<ICollection<MaintenanceRequest>>> GetNextWeekRequests(int id)
        {
            var mrs = await _maintenanceRepo.GetNextWeekRequests(id);
            return Ok(mrs);
        }
        [HttpGet("today")]
        public async Task<ActionResult<ICollection<MaintenanceRequest>>> GetTodayRequests(int id)
        {
            var mrs = await _maintenanceRepo.GetTodayRequests(id);
            return Ok(mrs);
        }
        [HttpPost("create")]
        public async Task<ActionResult<MaintenanceRequest>> CreateShedule(MaintanceCreateDTO dto)
        {
            var mr = await _maintenanceRepo.Create(dto);
            return Ok(mr);
        }

        [HttpPut("completeRequest")]
        public async Task<ActionResult> CompleteCurrentRequest(BaseDTO dto)
        {
            await _maintenanceRepo.CompleteMaintenanceRequest(dto.Id);
            //await _maintenanceRepo.CreateHistoryRercord(id);
            return Ok();
        }

        [HttpGet("history/records")]
        public async Task<ActionResult<ICollection<MaintenanceHistory>>> GetHistoryRecords(int id)
        {
            var records = await _maintenanceRepo.GetHistoryCompleteRecords(id);
            return Ok(records);
        }
        [HttpGet("history/records/all")]
        public async Task<ActionResult<ICollection<MaitenanceHistoryGetManyDTO>>> GetAllHistoryRecords(int id)
        {
            var records = await _maintenanceRepo.GetHardwareAllHistory(id);
            return Ok(records);
        }
        [HttpGet("history/records/all/ordered")]
        public async Task<ActionResult<ICollection<MaitenanceHistoryGetManyDTO>>> GetOrderedHistory(int id)
        {
            var records = await _maintenanceRepo.GetHardwareAllHistoryFilteref(id);
            return Ok(records);
        }
    }
}
