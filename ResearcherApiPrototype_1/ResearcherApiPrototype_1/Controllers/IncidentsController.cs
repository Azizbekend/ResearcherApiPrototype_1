using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs.IncidentDTOs;
using ResearcherApiPrototype_1.Models;
using ResearcherApiPrototype_1.Repos.IncidentRepo;

namespace ResearcherApiPrototype_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        private readonly IIncidentRepo _repo;

        public IncidentsController(IIncidentRepo repo)
        {
            _repo = repo;
        }
        [HttpGet("incidents/all")]
        public async Task<ActionResult<ICollection<Incident>>> GetAllIncidents()
        {
            var incidents = await _repo.GetAllIncidents();
            return Ok(incidents);
        }

        [HttpGet("incidents/object")]
        public async Task<ActionResult<ICollection<Incident>>> GetByObjectId(int id)
        {
            var incidents = await _repo.GetIncidentsByObjectId(id);
            return Ok(incidents);
        }

        [HttpGet("object/forTable")]
        public async Task<ActionResult<ICollection<CommonIncidentTableGetDTO>>> GetTableIncidentsObject(int id)
        {
            var incidents = await _repo.GetIncidentsByObjectIdForTable(id);
            return Ok(incidents);
        }
        [HttpGet("all/forTable")]
        public async Task<ActionResult<ICollection<CommonIncidentTableGetDTO>>> GetTableIncidentsAll()
        {
            var incidents = await _repo.GetIncidentsForTable();
            return Ok(incidents);
        }

        [HttpGet("hardware/all")]
        public async Task<ActionResult<ICollection<HardwareIncidentGetDTO>>> GetHardwareiIncident(int id)
        {
            var incidents = _repo.GetHardwareIncidents(id);

            if(incidents == null)
                return NotFound();
            else
                return Ok(incidents);
        }
    }
}
