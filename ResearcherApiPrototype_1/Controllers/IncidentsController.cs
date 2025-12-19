using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var incidents = _repo.GetAllIncidents();
            return Ok(incidents);
        }

        [HttpGet("incidents/object")]
        public async Task<ActionResult<ICollection<Incident>>> GetByObjectId(int id)
        {
            var incidents = _repo.GetIncidentsByObjectId(id);
            return Ok(incidents);
        }
    }
}
