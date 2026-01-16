using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs.CommonServisesDTOs;
using ResearcherApiPrototype_1.Repos.ServiceRequestRepo;

namespace ResearcherApiPrototype_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplyRequestController : ControllerBase
    {
        private readonly IServiceRepo _serviceRepo;

        public SupplyRequestController(IServiceRepo serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }

        [HttpPost("mainEngineer/SupplyRequest/InitialCreate")]
        public async Task<IActionResult> InitialCreateSupplyRequest(SupplyRequestInitialCreateDTO dto)
        {
            try
            { 
                var request = new CreateRequestME_DTO
                {
                    Title = $"Поставка: {dto.ProductName}",
                    CreatorId = dto.CreatorId,
                    HardwareId = dto.HardwareId,
                    ObjectId = dto.ObjectId,
                    Type = "InitialSupply"

                };
                var reqBD = await _serviceRepo.CreateServiceRequest(request);
                var stage = new CreateStageME_DTO
                {
                    CreatorId = dto.CreatorId,
                    ImplementerId = dto.CurrentImplementerId,
                    Discription = $"Необходима поставка материалов: {dto.ProductName} в количестве {dto.RequiredCount}",
                    ServiceId = reqBD.Id,
                    StageType = "InitialSupply"

                };
                await _serviceRepo.CreateRequestStage(stage);
                var supply = await _serviceRepo.CreateSupplyRequest(dto, reqBD.Id);
                //await _serviceRepo.CreateSupplyServiceLink(reqBD.Id, supply.Id);
                return Ok(supply);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("mainEngineer/supplyRequest/stage/create")]
        public async Task<IActionResult> CreateSupplyStage(SupplyRequestInStagesDTO dto)
        {
            var stage = new CreateStageME_DTO
            {
                CreatorId = dto.CreatorId,
                ImplementerId = dto.CurrentImplementerId,
                Discription = $"Необходима поставка материалов: {dto.ProductName} в количестве {dto.RequiredCount}",
                ServiceId = dto.ServiceId,
                StageType = "Supply"
            };
            await _serviceRepo.CreateSupplyRequestStage(stage);
            var newSupplyReq = new SupplyRequestInitialCreateDTO
            {
                CreatorId = dto.CreatorId,
                CurrentImplementerId = dto.CurrentImplementerId,
                ProductName = dto.ProductName,
                RequiredCount = dto.RequiredCount,
            };
            await _serviceRepo.CreateSupplyRequest(newSupplyReq, dto.ServiceId);
            return Ok();
        }

        [HttpPost("supplier/attachExpenses")]
        public async Task<IActionResult> AttachEpnensesStage(SupplyRequestAttachExpenseDTO dto)
        {
            await _serviceRepo.SupplyRequestAttachExpUpdate(dto);
            return Ok();

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceRepo.DeleteSupplyRequest(id);
            return Ok();
        }
    }
}
