using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs.CommonServisesDTOs;
using ResearcherApiPrototype_1.DTOs.CommonServisesDTOs.SupplyDTOs;
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
                    CreatorsCompanyId = dto.CreatorsCompanyId,
                    HardwareId = dto.HardwareId,
                    ObjectId = dto.ObjectId,
                    Type = "Поставочная"

                };
                var reqBD = await _serviceRepo.CreateServiceRequest(request);
                var stage = new CreateStageME_DTO
                {
                    CreatorId = dto.CreatorId,
                    CreatorsCompanyId = dto.CreatorsCompanyId,
                    ImplementerId = dto.NextImplementerId,
                    ImplementersCompanyId = dto.NextImplementerCompanyId,
                    Discription = $"Необходима поставка материалов: {dto.ProductName} в количестве {dto.RequiredCount}",
                    ServiceId = reqBD.Id,
                    StageType = "InitialSupply"

                };
                var stagenew = await _serviceRepo.CreateRequestStage(stage);
                var supply = await _serviceRepo.CreateSupplyRequest(dto, reqBD.Id);
                await _serviceRepo.CreateSupplyServiceLink(stagenew.Id, supply.Id);
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
            try
            {
                
                var stage = new CreateStageME_DTO
                {
                    CreatorId = dto.CreatorId,
                    ImplementerId = dto.NextImplementerId,
                    ImplementersCompanyId = dto.NextImplementerCompanyId,
                    Discription = $"Необходима поставка материалов: {dto.ProductName} в количестве {dto.RequiredCount}",
                    ServiceId = dto.ServiceId,
                    StageType = "Supply"
                };
                var res = await _serviceRepo.CreateSupplyRequestStage(stage);
                var newSupplyReq = new SupplyRequestInitialCreateDTO
                {
                    CreatorId = dto.CreatorId,
                    CreatorsCompanyId = dto.CreatiorCompanyId,
                    ProductName = dto.ProductName,
                    RequiredCount = dto.RequiredCount,
                };
                await _serviceRepo.CreateSupplyRequest(newSupplyReq, dto.ServiceId);
                await _serviceRepo.CreateSupplyServiceLink(res.Id, stage.ServiceId);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("mainEngineer/supplyRequest/stage/resend")]
        public async Task<IActionResult> CreateSupplyStage(SupplyRequestResendStagesDTO dto)
        {
            var stage = new CreateStageME_DTO
            {               
                CreatorId = dto.CreatorId,
                ImplementerId = dto.NextImplementerId,
                ImplementersCompanyId = dto.NextImplementerCompanyId,
                Discription = dto.ResendDiscription,
                ServiceId = dto.ServiceId,
                StageType = "Supply"
            };
            var res = await _serviceRepo.CreateSupplyRequestStage(stage);
            await _serviceRepo.CreateSupplyServiceLink(res.Id, stage.ServiceId);
            return Ok();
        }

        [HttpPost("supplier/attachExpenses")]
        public async Task<IActionResult> AttachEpnensesStage(SupplyRequestAttachExpenseDTO dto)
        {
            await _serviceRepo.SupplyRequestAttachExpUpdate(dto);
            await _serviceRepo.CreateSupplyServiceLink(dto.StageId, dto.RequestId);
            return Ok();
        }
        [HttpPost("supplier/warehouse/confirm/noPay")]
        public async Task <IActionResult> ConfirmWarehouse(SupplyRequestConfirmWarehouseDTO dto)
        {
            await _serviceRepo.SupplyRequestWarehouseConfirm(dto);
            await _serviceRepo.CreateSupplyServiceLink(dto.StageId, dto.RequestId);
            return Ok();
        }

        [HttpPost("buhgalteriya/attachPay")]
        public async Task<IActionResult> AttachPay(SupplyRequestAttachPay dto)
        {
            await _serviceRepo.SupplyRequestAttachPay(dto);
            await _serviceRepo.CreateSupplyServiceLink(dto.StageId, dto.RequestId);
            return Ok();
        }

        [HttpPost("supplier/warehouse/confirm")]
        public async Task<IActionResult> ConfirmWarehouseSupply(SupplyWarehouseConfirmDTO dto)
        {
            await _serviceRepo.SupplyRequestConfirmWarehouseSupply(dto);
            await _serviceRepo.CreateSupplyServiceLink(dto.StageId, dto.RequestId);
            return Ok();
        }
        [HttpPost("mainEngineer/supplyStage/complete")]
        public async Task<IActionResult> CompleteSuppleStage(CompleteSupplyStageDTO dto)
        {
            await _serviceRepo.ConfirmSupplyStage(dto);        
            return Ok();
        }

        [HttpPost("mainEngineer/supplyStage/Cancel")]
        public async Task<IActionResult> CancelSupplyStage(CancelSupplyStageDTO dto)
        {
            await _serviceRepo.CancelSupplyStage(dto);
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
