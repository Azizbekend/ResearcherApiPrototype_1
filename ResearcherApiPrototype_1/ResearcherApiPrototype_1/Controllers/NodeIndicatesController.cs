using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs.NodesDTOs;
using ResearcherApiPrototype_1.Models;
using ResearcherApiPrototype_1.Repos.NodeIndicatesRepo;

namespace ResearcherApiPrototype_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodeIndicatesController : ControllerBase
    {
        private readonly INodeIndicatesRepo _nodeIndicatesRepo;

        public NodeIndicatesController(INodeIndicatesRepo nodeIndicatesRepo)
        {
            _nodeIndicatesRepo = nodeIndicatesRepo;
        }

        [HttpGet("all")]
        public async Task<ActionResult<ICollection<NodeIndicates>>> Get(int id)
        {
            var indicates = await _nodeIndicatesRepo.GetIndicatesByNodeIdAsync(id);
            return Ok(indicates);
        }

        [HttpGet("actual")]
        public async Task<ActionResult<NodeIndicates>> GetLast(int id)
        {
            var indicates = await _nodeIndicatesRepo.GetLastIndecatesByNodeIdAsync(id);
            return Ok(indicates);
        }

        [HttpGet("actual/plcNodeOd")]
        public async Task<ActionResult<NodeIndicates>> GetLastByNodeId(string id)
        {
            if (id.StartsWith("ns=4"))
            {
                var indicates = await _nodeIndicatesRepo.GetIndicatesByPlcNodeIdAsync(id);
                return Ok(indicates);
            }
            else
                return NotFound();
        }
        [HttpGet("technicalChars/Shapshi")]
        public async Task<ActionResult<ForPasportDTO>> GetCharsForPasportShap()
        {
            var cahrs = await _nodeIndicatesRepo.GetBaseReadingsSha();
            return Ok(cahrs);
        }

        [HttpPost("actual/group")]
        public async Task<ActionResult<ICollection<NodeIndecatesGroupResponseDTO>>> GetGroup(IndicatesGroupRequestDTO dto)
        {
            var group = await _nodeIndicatesRepo.GetIndicatesByList(dto.listId);
            return Ok(group);
        }

        [HttpGet("internal/findByend")]
        public async Task<ActionResult<NodeIndicates>> GetByEnd(string end)
        {
            var indicates = await _nodeIndicatesRepo.GetByStrEnd(end);
            return Ok(indicates);
        }

        [HttpGet("internal/findGroupByend")]
        public async Task<ActionResult<ICollection<NodeIndicates>>> GetGroupByEnd(string end)
        {
            var indicates = await _nodeIndicatesRepo.GetGroupByStrEnd(end);
            return Ok(indicates);
        }
    }
}
