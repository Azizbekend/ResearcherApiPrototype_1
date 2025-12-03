using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Models;
using ResearcherApiPrototype_1.Repos.HardwareRepo;

namespace ResearcherApiPrototype_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HardwareController : ControllerBase
    {
        private readonly IHardwareRepo _hardwareRepo;

        public HardwareController(IHardwareRepo hardwareRepo)
        {
            _hardwareRepo = hardwareRepo;
        }


        [HttpGet("all")]
        public async Task<ActionResult<ICollection<HardwareInfo>>> GetAll()
        {
            var hw = await _hardwareRepo.GetAllHardwaresAsync();
            return Ok(hw);
        }

        [HttpGet("infoAll")]
        public async Task<ActionResult<ICollection<HardwareInfo>>> Get(int id)
        {
            var hw = await _hardwareRepo.GetHardwaresByControlBlockIdAsync(id);
            return Ok(hw);
        }
        [HttpGet("infoSingle")]
        public async Task<ActionResult<HardwareInfo>> GetById(int id)
        {
            var hw = await _hardwareRepo.GetHardwareByIdAsync(id);
            return Ok(hw);
        }
        [HttpPost("create")]
        public async Task<ActionResult<int>> CreateOne([FromBody]HardwareCreateDTO hw)
        {
            var hwtr = await _hardwareRepo.CreateHardwareAsync(hw);
            return Ok(hwtr.Id);
        }
        [HttpPost("Activate")]
        public async Task<ActionResult> Activating(int id)
        {
            await _hardwareRepo.HardwareActivating(id);
            return Ok();
        }
    }
}
