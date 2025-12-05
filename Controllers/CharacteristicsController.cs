using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Models;
using ResearcherApiPrototype_1.Repos.CharacteristicRepo;

namespace ResearcherApiPrototype_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacteristicsController : ControllerBase
    {
        private ICharRepo _charRepo;

        public CharacteristicsController(ICharRepo chaRepo)
        {
            _charRepo = chaRepo;
        }
        [HttpGet("characteristics")]
        public async Task<ActionResult<ICollection<HardwareCharacteristic>>> GetByHardwareId(int id)
        {
            var characteristic = await _charRepo.FindByHardwareId(id);
            return Ok(characteristic);
        }

        [HttpPost("createOne")]
        public async Task<ActionResult<CharCreateDTO>> CreateOne([FromBody]CharCreateDTO characterCreateDTO)
        {
            await _charRepo.Create(characterCreateDTO);
            return Ok(characterCreateDTO);
        }

        [HttpPost("createMany")]
        public async Task<ActionResult> CreateMany([FromBody] CharMassCreateDTO characterMassCreateDTO)
        {
            await _charRepo.CreateMass(characterMassCreateDTO);
            return Ok();
        }
        [HttpPut("update")]
        public async Task<ActionResult> Update(CharUpdateDTO characterUpdateDTO)
        {
            var ci = await _charRepo.UpdateInfo(characterUpdateDTO);
            return Ok(ci);
        }
        [HttpDelete("characteristic/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _charRepo.Delete(id);
            return Ok();
        }

    }
}
