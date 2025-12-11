using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs;
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
            var indicates = await _nodeIndicatesRepo.GetIndicatesByPlcNodeIdAsync(id);
            return Ok(indicates);
        }

        [HttpPost("actual/group")]
        public async Task<ActionResult<ICollection<NodeIndecatesGroupResponseDTO>>> GetGroup(IndicatesGroupRequestDTO dto)
        {

            Console.WriteLine($"GetGroup: получено {dto.listId?.Count ?? 0} ID");
            
            var group = await _nodeIndicatesRepo.GetIndicatesByList(dto.listId);
            
            Console.WriteLine($"Вернулось {group?.Count ?? 0} записей");

            return Ok(group);
        }
    }
}
