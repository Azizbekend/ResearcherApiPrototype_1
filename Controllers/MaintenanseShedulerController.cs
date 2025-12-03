using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs;
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
        [HttpPost("create")]
        public async Task<ActionResult<MaintenanceRequest>> CreateShedule(MaintanceCreateDTO dto)
        {
            var mr = await _maintenanceRepo.Create(dto);
            return Ok(mr);
        }

        [HttpPut("completeRequest")]
        public async Task<ActionResult> CompleteCurrentRequest(BaseDTO dto)
        {
            try
            {
                // Добавляем валидацию входных данных
                if (dto == null)
                {
                    return BadRequest(new { error = "Request body cannot be null" });
                }
                
                if (dto.Id <= 0)
                {
                    return BadRequest(new { error = "Invalid ID provided. ID must be greater than 0" });
                }
                
                await _maintenanceRepo.CompleteMaintenanceRequest(dto.Id);
                return Ok(new { message = "Maintenance request completed successfully" });
            }
            catch (Exception ex) when (ex.Message == "Maintenance request not found!")
            {
                // Обрабатываем специфическое исключение из репозитория
                return NotFound(new { 
                    error = $"Maintenance request with ID {dto?.Id} not found",
                    details = "The requested maintenance record does not exist or has already been completed"
                });
            }
            catch (Exception ex)
            {
                // Общая обработка ошибок
                _logger.LogError(ex, "Error completing maintenance request for ID: {Id}", dto?.Id);
                return StatusCode(500, new { 
                    error = "An error occurred while processing your request",
                    requestId = HttpContext.TraceIdentifier 
                });
            }
        }    
    }
}
