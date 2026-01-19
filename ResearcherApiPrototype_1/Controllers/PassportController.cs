using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.DTOs.ObjectDTOs;
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
        [HttpGet("single")]
        public async Task<IActionResult> GetObject(int id)
        {
            var obj = _objectPassportRepo.GetSingleById(id);
            return Ok(obj);
        }
        [HttpGet("object/companies")]
        public async Task<ActionResult<ICollection<ObjectCompanyLink>>> GetObjectCompanies(int id)
        {
            var list = await _objectPassportRepo.GetObjectCompanies(id);
            return Ok(list);
        }
        [HttpGet("object/company/users")]
        public async Task<ActionResult<ICollection<ObjectCompanyLink>>> GetObjectCompanyUsers(int id)
        {
            var list = await _objectPassportRepo.GetUsersCompany(id);
            return Ok(list);
        }
        [HttpPost("create")]
        public async Task<ActionResult<int>> Create([FromBody]StaticObjectInfo dto)
        {
            var passport = await _objectPassportRepo.Create(dto);
            return Ok(passport.Id);
        }

        [HttpPost("object/company/attach")]
        public async Task<IActionResult> AttachCompany(AttachCompanyToObjectDTO dto)
        {
            await _objectPassportRepo.AttachCompany(dto);
            return Ok();
        }

        [HttpPost("object/company/user/attach")]
        public async Task<IActionResult> AttachUser(AttachUserToObjectLinkDTO dto)
        {
            var a = await _objectPassportRepo.AttachUser(dto);
            UpdateObjAccessesDTO acesses = new UpdateObjAccessesDTO()
            {
                UserID = a.UserId,
                ObjectCompanyLinkId = a.ObjectCompanyLinkId,
                Is3DEnabled = false,
                IsCommandsEnabled = false,
                IsNodeInfosEnabled = false,
                CanCreateIncidentServiseRequests = false,
                CanCreateCommonServiceRequests = false
            };
            await _objectPassportRepo.UpdateUsersAccesses(acesses);
            return Ok();
        }

    }
}
