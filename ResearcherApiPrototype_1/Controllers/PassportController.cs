using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Models;
using ResearcherApiPrototype_1.Repos.ObjectPassportRepo;

namespace ResearcherApiPrototype_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassportController : ControllerBase
    {
        private readonly IObjectPassportRepo _objectPassportRepo;

        public PassportController(IObjectPassportRepo objectPassportRepo)
        {
            _objectPassportRepo = objectPassportRepo;
        }

        [HttpGet("all")]
        public async Task<ActionResult<ICollection<StaticObjectInfo>>> GetAll()
        {
            var all = await _objectPassportRepo.GetAll();
            return Ok(all);
        }

        [HttpPost("create")]
        public async Task<ActionResult<int>> Create([FromBody]StaticObjectInfo dto)
        {
            var passport = await _objectPassportRepo.Create(dto);
            return Ok(passport.Id);
        }

    }
}
