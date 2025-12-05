using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Models;
using ResearcherApiPrototype_1.Repos.NodeRepo;

namespace ResearcherApiPrototype_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodeInfoController : ControllerBase
    {
        private readonly INodeRepo _nodeRepo;

        public NodeInfoController(INodeRepo nodeRepo)
        {
            _nodeRepo = nodeRepo;
        }

        [HttpGet("infos")]
        public async Task<ActionResult<ICollection<NodeInfo>>> GetInfos(int id) 
        {
            var nodes = await _nodeRepo.GetAllInfoNodesByHardwareId(id);
            return Ok(nodes);
        }
        [HttpGet("commands")]
        public async Task<ActionResult<ICollection<NodeInfo>>> GetCommands(int id)
        {
            var nodes = await _nodeRepo.GetAllCommandNodesByHardwareId(id);
            return Ok(nodes);
        }
        [HttpGet("all")]
        public async Task<ActionResult<ICollection<NodeInfo>>> GetAll()
        {
            var nodes = await _nodeRepo.GetAllNodesAsync();
            return Ok(nodes);
        }

        [HttpPost("createInfo")]
        public async Task<ActionResult<int>> CreateInfoNode(NodeCreateDTO dto)
        {
            var newNode = await _nodeRepo.CreateInfoNodeAsync(dto.Name, dto.PlcNodeId, dto.Mesurement, dto.HardwareId);
            return Ok(newNode.Id);
        }
        [HttpPost("createCommand")]
        public async Task<ActionResult<int>> CreateCommandNode(NodeCreateDTO dto)
        {
            var newNode = await _nodeRepo.CreateCommandNodeAsync(dto.Name, dto.PlcNodeId, dto.Mesurement, dto.IsValue, dto.HardwareId);
            return Ok(newNode.Id);
        }
        [HttpPost("createMassInfo")]
        public async Task<ActionResult> CreateMassInfoNode(MassNodeCreateDTO dto)
        {
            await _nodeRepo.CreateMassInfoNodeAsync(dto);
            return Ok();
        }
        [HttpPost("createMassCommand")]
        public async Task<ActionResult> CreateMassCommandNode(MassNodeCreateDTO dto)
        {
            await _nodeRepo.CreateMassCommandNodeAsync(dto);
            return Ok();
        }

        [HttpPut("info/update")]
        public async Task<ActionResult> UpdareNodeInfo(NodeUpdateDTO dto)
        {
            var ni=await _nodeRepo.UpdateNode(dto);
            return Ok(ni);
        }

    }
}
