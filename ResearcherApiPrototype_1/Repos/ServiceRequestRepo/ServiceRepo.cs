using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1.DTOs.CommonServisesDTOs;
using ResearcherApiPrototype_1.DTOs.CommonServisesDTOs.SupplyDTOs;
using ResearcherApiPrototype_1.Models.ServiceRequests;

namespace ResearcherApiPrototype_1.Repos.ServiceRequestRepo
{
    public class ServiceRepo : IServiceRepo
    {
        private readonly AppDbContext _context;

        public ServiceRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CancelRequest(CompleteCancelRequestME_DTO dto)
        {
            var request = await _context.CommonRequests.FirstOrDefaultAsync(x => x.Id == dto.RequestId);
            if (request != null) 
            {
                request.Status = "Canceled";
                request.ImplementerId = dto.ImplementerId;
                request.ClosedAt = DateTime.Now.ToUniversalTime();
                _context.CommonRequests.Attach(request);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CancelStageME(CancelStageME_DTO dto)
        {
            var stage = await _context.RequestStages.FirstOrDefaultAsync(x => x.Id == dto.StageId);
            if (stage != null)
            {
                stage.CurrentStatus = "Canceled";
                stage.CancelDiscription = dto.CancelDiscriprion;
                stage.ClosedAt = DateTime.Now.ToUniversalTime();
                _context.RequestStages.Attach(stage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CompleteRequest(CompleteCancelRequestME_DTO dto)
        {
            var check = await _context.RequestStages.FirstOrDefaultAsync(x => x.ServiceId == dto.RequestId && x.CurrentStatus == "New");
            if (check == null)
            {
                var request = await _context.CommonRequests.FirstOrDefaultAsync(x => x.Id == dto.RequestId && x.Status != "Completed");
                if (request != null)
                {
                    request.Status = "Completed";
                    request.ClosedAt = DateTime.Now.ToUniversalTime();
                    request.ImplementerId = dto.ImplementerId;
                    _context.CommonRequests.Attach(request);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Current request already completed");
                }
            }
            else
                throw new Exception("Request has not completed stages! Try again when all stages will in compleded status");
            
        }
        private async Task InnerCompleteStage(int stageId, string discription, int implementerId)
        {
            var stage = await _context.RequestStages.FirstOrDefaultAsync(x => x.Id == stageId);
            if (stage != null)
            {
                stage.CurrentStatus = "Completed";
                stage.ClosedAt = DateTime.Now.ToUniversalTime();
                _context.RequestStages.Attach(stage);
                var newStage = new CommonRequestStage()
                {
                    ServiceId = stage.ServiceId,
                    CreatorId = stage.ImplementerId,
                    ImplementerId = implementerId,
                    StageType = "Supply",
                    Discription = discription
                };
                _context.RequestStages.Add(newStage);
                await _context.SaveChangesAsync();
            }
        }
        public async Task CompleteStage(CompleteStageDTO dto)
        {
            var stage = await _context.RequestStages.FirstOrDefaultAsync(x => x.Id == dto.StageId);
            if(stage != null)
            {
                stage.CurrentStatus = "Completed";
                stage.ClosedAt = DateTime.Now.ToUniversalTime();
                _context.RequestStages.Attach(stage);
                var newStage = new CommonRequestStage()
                {
                    ServiceId = stage.ServiceId,
                    CreatorId = stage.ImplementerId,
                    ImplementerId = stage.CreatorId,
                    StageType = "New",
                    Discription = dto.Discription
                };
                _context.RequestStages.Add(newStage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CompleteStageME(CompleteStageME_DTO dto)
        {
            var stage = await _context.RequestStages.FirstOrDefaultAsync(x => x.Id == dto.StageId);
            if (stage != null && stage.ImplementerId == dto.EngineerId)
            {
                stage.CurrentStatus = "Completed";
                _context.RequestStages.Attach(stage);
                await _context.SaveChangesAsync();
            }
            else
                throw new Exception("Only implementer can comlete current stage!");
        }

        public async Task<CommonServiceRequest> CreateIncidentServiceRequest(CreateIncidentServiceRequestDTO dto)
        {
            var newIncidentRequest = new CommonServiceRequest()
            {
                Status = "New",
                Title = dto.Title,
                Type = "Incident",
                CreatorId = dto.CreatorId,
                HardwareId = dto.HardwareId,
                ObjectId = dto.ObjectId
            };
            var a = _context.CommonRequests.Add(newIncidentRequest);
            await _context.SaveChangesAsync();
            return newIncidentRequest;
        }
        public async Task<CommonRequestStage> CreateInitialRequestStage(CreateStageME_DTO dto)
        {
            
                var newStage = new CommonRequestStage()
                {
                    ServiceId = dto.ServiceId,
                    CreatorId = dto.CreatorId,
                    ImplementerId = dto.ImplementerId,
                    StageType = dto.StageType,
                    Discription = dto.Discription
                };
                _context.RequestStages.Add(newStage);
                await _context.SaveChangesAsync();
                return newStage;
            
        }
        public async Task<CommonRequestStage> CreateRequestStage(CreateStageME_DTO dto)
        {
            var buff = await _context.RequestStages.Where(x => x.ServiceId == dto.ServiceId).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if(buff != null && buff.CurrentStatus == "New")
            {
                throw new Exception("Can not create new stage while Request has not completed stage!");
            }
            else
            {
                var newStage = new CommonRequestStage()
                {
                    ServiceId = dto.ServiceId,
                    CreatorId = dto.CreatorId,
                    ImplementerId = dto.ImplementerId,
                    StageType = dto.StageType,
                    Discription = dto.Discription
                };
                _context.RequestStages.Add(newStage);
                await _context.SaveChangesAsync();
                return newStage;
            }

        }
        public async Task<CommonRequestStage> CreateSupplyRequestStage(CreateStageME_DTO dto)
        {
            var buff = await _context.RequestStages.Where(x => x.ServiceId == dto.ServiceId).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (buff != null && buff.CurrentStatus == "New")
            {
                throw new Exception("Can not create new stage while Request has not completed stage!");
            }
            else
            {
                var newStage = new CommonRequestStage()
                {
                    ServiceId = dto.ServiceId,
                    CreatorId = dto.CreatorId,
                    ImplementerId = dto.ImplementerId,
                    StageType = dto.StageType,
                    Discription = dto.Discription
                };
                _context.RequestStages.Add(newStage);
                await _context.SaveChangesAsync();
                return newStage;
            }
          
        }
        public async Task<CommonServiceRequest> CreateServiceRequest(CreateRequestME_DTO dto)
        {
            var newRequest = new CommonServiceRequest()
            {
                Title = dto.Title,
                Status = "New",
                Type = dto.Type,
                CreatorId = dto.CreatorId,
                HardwareId = dto.HardwareId,
                ObjectId = dto.ObjectId
            };
            _context.CommonRequests.Add(newRequest);
            await _context.SaveChangesAsync();
            return newRequest;
        }

        public async Task<ICollection<CommonServiceRequest>> GetAllServiceRequestsAsync()
        {
            return await _context.CommonRequests.ToListAsync();
        }

        public async Task<ICollection<CommonRequestStage>> GetRequestStagesAsync(int id)
        {
            return await _context.RequestStages.Where(x => x.ServiceId == id).ToListAsync();
        }

        public async Task CreateIncidentLink(int requestId, int incidentId)
        {
            var newIncidentServiceLink = new IncidentServiceLink()
            {
                IncidentId = incidentId,
                ServiceRequestId = requestId
            };
            _context.IncidentServiceLinks.Add(newIncidentServiceLink);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<CommonServiceRequest>> GetAllObjectRequests(int id)
        {
            return await _context.CommonRequests.Where(x => x.ObjectId == id).ToListAsync();
        }

        public async Task<ICollection<CommonRequestStage>> GetAllUsersStages(int id)
        {
            return await _context.RequestStages.Where(x => x.ImplementerId == id).ToListAsync();
        }

        public async Task<SupplyRequest> CreateSupplyRequest(SupplyRequestInitialCreateDTO dto, int serviceId)
        {

            var supplyReq = new SupplyRequest
            {
                CreatorId = dto.CreatorId,
                ProductName = dto.ProductName,
                CurrentImplementerId = dto.CurrentImplementerId,
                RequiredCount = dto.RequiredCount,
                CommonRequestId = serviceId,
                CurrentStatus = "New"
            };
            _context.SupplyRequests.Add(supplyReq);
            await _context.SaveChangesAsync();
            return supplyReq;
        }

        

        public async Task CreateSupplyServiceLink(int serviceId, int supplyId)
        {
        }
        public async Task DeleteSupplyRequest(int id)
        {
            var req = await _context.SupplyRequests.FirstOrDefaultAsync(x => x.Id == id);
            _context.SupplyRequests.Remove(req);
            await _context.SaveChangesAsync();
        }
        public async Task SupplyRequestAttachExpUpdate(SupplyRequestAttachExpenseDTO dto)
        {
            var supplyRequest = await _context.SupplyRequests.FirstOrDefaultAsync(x => x.Id == dto.SupplyRequestId);
            if (supplyRequest != null) 
            {
                supplyRequest.SupplierName = dto.SupplierName;
                supplyRequest.RealCount = dto.RealCount;
                supplyRequest.ExpenseNumber = dto.ExpenseNumber;
                supplyRequest.CurrentImplementerId = dto.CurrentImplementerId;
                supplyRequest.Expenses = dto.Expenses;
                supplyRequest.IsPayed = false;
                supplyRequest.CurrentStatus = "Счет получен";
                _context.SupplyRequests.Attach(supplyRequest);
                await _context.SaveChangesAsync();
                await InnerCompleteStage(dto.StageId, $"Выставлен счет #{supplyRequest.ExpenseNumber} на сумму: {supplyRequest.Expenses}. Поставщик: ${supplyRequest.SupplierName}", supplyRequest.CurrentImplementerId);
            }
        }

        public async Task SupplyRequestAttachPay(SupplyRequestAttachPay dto)
        {
            var supplyRequest = await _context.SupplyRequests.FirstOrDefaultAsync(x => x.Id == dto.SupplyRequestId);
            if (supplyRequest != null)
            {
                supplyRequest.CurrentImplementerId= dto.CurrentImplementerId;
                supplyRequest.CurrentStatus = $"Счет #{supplyRequest.ExpenseNumber} оплачен.";
                supplyRequest.IsPayed= true;
                _context.SupplyRequests.Attach(supplyRequest);
                await _context.SaveChangesAsync();
                await InnerCompleteStage(dto.StageId, $"Счет #{supplyRequest.ExpenseNumber} оплачен. Ожидается поставка на склад.", supplyRequest.CurrentImplementerId);
            }

        }

        public async Task<bool> IsServiceRequestExists(int id)
        {
            var request = _context.CommonRequests.FirstOrDefaultAsync(_ => _.Id == id);
            if (request != null)
                return true;
            return false;

        }

        public async Task SupplyRequestConfirmWarehouseSupply(SupplyWarehouseConfirmDTO dto)
        {
            var supplyRequest = await _context.SupplyRequests.FirstOrDefaultAsync(x => x.Id == dto.SupplyRequestId);
            if (supplyRequest != null)
            {
                supplyRequest.CurrentStatus = "Прибыло на склад";
                supplyRequest.CurrentImplementerId = dto.CurrentImplementerId;
                _context.SupplyRequests.Attach(supplyRequest);
                await _context.SaveChangesAsync();
                await InnerCompleteStage(dto.StageId, $"Материал: {supplyRequest.ProductName} в количестве {supplyRequest.RealCount} прибыл на склад. Осущестлвяется поставка на объект", supplyRequest.CurrentImplementerId);
            }
        }

        public async Task ConfirmSupplyStage(CompleteSupplyStageDTO dto)
        {
            var supplyRequest = await _context.SupplyRequests.FirstOrDefaultAsync(x => x.Id == dto.SupplyRequestId);
            if (supplyRequest != null)
            {
                supplyRequest.CurrentImplementerId = dto.ImplementerId;
                supplyRequest.CurrentStatus = "Поставка завершена.";
                _context.SupplyRequests.Attach(supplyRequest);
                var stage = await _context.RequestStages.FirstOrDefaultAsync(x => x.Id == dto.SupplyStageId);
                stage.CurrentStatus = "Completed";
                stage.ClosedAt = DateTime.Now.ToUniversalTime();
                _context.RequestStages.Attach(stage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CancelSupplyStage(CancelSupplyStageDTO dto)
        {
            var supplyRequest = await _context.SupplyRequests.FirstOrDefaultAsync(x => x.Id == dto.SupplyRequestId);
            if(supplyRequest != null)
            {
                supplyRequest.CurrentStatus = "Canceled";
                _context.SupplyRequests.Attach(supplyRequest);
                var stage = await _context.RequestStages.FirstOrDefaultAsync(x => x.Id == dto.SupplyStageId);
                stage.CurrentStatus = "Canceled";stage.CancelDiscription = dto.CancelDiscription;
                _context.RequestStages.Attach(stage);
                await _context.SaveChangesAsync();
            }
        }
    }
}
