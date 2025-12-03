using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Models;
using ResearcherApiPrototype_1.Repos.ControlBlockRepo;

namespace ResearcherApiPrototype_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlBlockController : ControllerBase
    {
        private readonly IControlBlockRepo _controlBlockRepo;

        public ControlBlockController(IControlBlockRepo controlBlockController)
        {
            _controlBlockRepo = controlBlockController;
        }


        [HttpGet("all")]
        public async Task<ActionResult<ICollection<ControlBlockInfo>>> GetAll() 
        {
            var cb = await _controlBlockRepo.GetAllControlBlocks();
            return Ok(cb);
        }

        [HttpGet("all/passport")]
        public async Task<ActionResult<ICollection<ControlBlockInfo>>> GetAllByPassport(int id)
        {
            var cb = await _controlBlockRepo.GetControlBlockInfoByPassportId(id);
            return Ok(cb);
        }

        [HttpPost("create")]
        public async Task<ActionResult<int>> Create(ControlBlockCreaterDTO dto)
        {
            var cb = await _controlBlockRepo.CreateControlBlock(dto.Name, dto.PlcIpAdress, dto.StaticObjectInfoId);
            return Ok(cb.Id);
        }
    }
}
