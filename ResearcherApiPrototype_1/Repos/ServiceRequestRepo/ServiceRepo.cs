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
                request.ImplementersCompanyId = dto.ImplementerCompanyId;
                //request.CancelDiscription = dto.
                request.ClosedAt = DateTime.Now.ToUniversalTime();
                _context.CommonRequests.Attach(request);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Current request not found!");
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
            else
            {
                throw new Exception("Current stage not found!");
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
                    request.ImplementersCompanyId = dto.ImplementerCompanyId;
                    _context.CommonRequests.Attach(request);
                    await _context.SaveChangesAsync();
                    if(request.Type == "Incident")
                    {
                        var incLink = await _context.IncidentServiceLinks.FirstOrDefaultAsync(x => x.ServiceRequestId == request.Id);
                        await InnerCompleteIncident(incLink.IncidentId);
                    }
                }
                else
                {
                    throw new Exception("Данная заявка уже выполнена!");
                }
            }
            else
                throw new Exception("Ошибка! В заявке есть незавершенные этапы! Попробуйте позже...");
            
        }
        private async Task<CommonRequestStage> InnerCompleteStage(int stageId, string discription, int implementerId, int nextImplementerCompanyId)
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
                    CreatorsCompanyId = stage.CreatorsCompanyId,
                    ImplementerId = implementerId,
                    ImplementersCompanyId = nextImplementerCompanyId,
                    StageType = "Supply",
                    Discription = discription
                };
                _context.RequestStages.Add(newStage);
                await _context.SaveChangesAsync();
                return newStage;
            }
            else
            {
                throw new Exception("Current stage not found!");
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
                    CreatorsCompanyId = stage.ImplementersCompanyId,
                    ImplementerId = stage.CreatorId,
                    ImplementersCompanyId = stage.CreatorsCompanyId,
                    StageType = "Общий",
                    Discription = dto.Discription
                };
                _context.RequestStages.Add(newStage);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Current stage not found!");
            }
        }

        public async Task<CommonRequestStage> CompleteStageME(CompleteStageME_DTO dto)
        {
            var stage = await _context.RequestStages.FirstOrDefaultAsync(x => x.Id == dto.StageId);
            if (stage != null && stage.ImplementerId == dto.EngineerId)
            {
                stage.CurrentStatus = "Completed";
                _context.RequestStages.Attach(stage);
                var newStage = new CommonRequestStage()
                {
                    ServiceId = stage.ServiceId,
                    CreatorId = stage.ImplementerId,
                    CreatorsCompanyId = stage.ImplementersCompanyId,
                    ImplementerId = stage.CreatorId,
                    ImplementersCompanyId = stage.CreatorsCompanyId,
                    StageType = "Общий",
                    CurrentStatus = "Completed",
                    Discription = dto.Discription
                };
                _context.RequestStages.Add(newStage);
                await _context.SaveChangesAsync();
                return newStage;
            }
            else
                throw new Exception("Ошибка! Только исполнитель может завершить текущий этап!");
        }

        public async Task<CommonServiceRequest> CreateIncidentServiceRequest(CreateIncidentServiceRequestDTO dto)
        {
            var newIncidentRequest = new CommonServiceRequest()
            {
                Status = "New",
                Title = dto.Title,
                Type = "Incident",
                CreatorId = dto.CreatorId,
                CreatorsCompanyId = dto.CreatorsCompanyId,
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
                    CreatorsCompanyId = dto.CreatorsCompanyId,
                    ImplementerId = dto.ImplementerId,
                    ImplementersCompanyId = dto.ImplementersCompanyId,
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
                throw new Exception("Ошибка! Невозможно создать новый этап пока есть незавершенные этапы!");
            }
            else
            {
                var newStage = new CommonRequestStage()
                {
                    ServiceId = dto.ServiceId,
                    CreatorId = dto.CreatorId,
                    CreatorsCompanyId= dto.CreatorsCompanyId,
                    ImplementerId = dto.ImplementerId,
                    ImplementersCompanyId = dto.ImplementersCompanyId,
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
                throw new Exception("Ошибка! Невозможно создать новый этап пока есть незавершенные этапы!");
            }
            else
            {
                var newStage = new CommonRequestStage()
                {
                    ServiceId = dto.ServiceId,
                    CreatorId = dto.CreatorId,
                    CreatorsCompanyId = dto.CreatorsCompanyId,    
                    ImplementerId = dto.ImplementerId,
                    ImplementersCompanyId = dto.ImplementersCompanyId,
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
                CreatorsCompanyId = dto.CreatorsCompanyId,
                HardwareId = dto.HardwareId,
                ObjectId = dto.ObjectId
            };
            _context.CommonRequests.Add(newRequest);
            await _context.SaveChangesAsync();
            return newRequest;
        }

        public async Task<ICollection<CommonServiceRequest>> GetAllServiceRequestsAsync()
        {
            return await _context.CommonRequests.OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<ICollection<CommonRequestStage>> GetRequestStagesAsync(int id)
        {
            return await _context.RequestStages.Where(x => x.ServiceId == id).OrderBy(x => x.Id).ToListAsync();
        }

        public async Task CreateIncidentLink(int requestId, int incidentId)
        {
            var newIncidentServiceLink = new IncidentServiceLink()
            {
                IncidentId = incidentId,
                ServiceRequestId = requestId
            };
            var inc = await _context.Incidents.FirstOrDefaultAsync(x => x.Id == incidentId);
            inc.Status = "В Работе";
            _context.Incidents.Attach(inc);
            _context.IncidentServiceLinks.Add(newIncidentServiceLink);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<CommonServiceRequest>> GetAllObjectRequests(int id)
        {
            return await _context.CommonRequests.Where(x => x.ObjectId == id).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<ICollection<CommonRequestStage>> GetAllUsersStages(int id)
        {
            return await _context.RequestStages.Where(x => x.ImplementerId == id).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<SupplyRequest> CreateSupplyRequest(SupplyRequestInitialCreateDTO dto, int serviceId)
        {

            var supplyReq = new SupplyRequest
            {
                CreatorId = dto.CreatorId,
                CreatorsCompanyId = dto.CreatorsCompanyId,
                ProductName = dto.ProductName,
                CurrentImplementerId = dto.NextImplementerId,
                ImplementersCompanyId = dto.NextImplementerCompanyId,
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
            var spsLink = await _context.SupplyRequestLinks.FirstOrDefaultAsync(x => x.SuppluStageId == dto.StageId);
            var supplyRequest = await _context.SupplyRequests.FirstOrDefaultAsync(x => x.Id == spsLink.SuppluRequestId);
            if (supplyRequest != null && supplyRequest.CurrentStatus == "New" ) 
            {
                supplyRequest.SupplierName = dto.SupplierName;
                supplyRequest.RealCount = dto.RealCount;
                supplyRequest.ExpenseNumber = dto.ExpenseNumber;
                supplyRequest.CurrentImplementerId = dto.NextImplementerId;
                supplyRequest.ImplementersCompanyId = dto.NextImplementerCompanyId;
                supplyRequest.Expenses = dto.Expenses;
                supplyRequest.IsPayed = false;
                supplyRequest.CurrentStatus = "Счет получен";
                _context.SupplyRequests.Attach(supplyRequest);
                await _context.SaveChangesAsync();
                var newStage = await InnerCompleteStage(dto.StageId, $"Выставлен счет #{supplyRequest.ExpenseNumber} на сумму: {supplyRequest.Expenses}. Поставщик: ${supplyRequest.SupplierName}", supplyRequest.CurrentImplementerId, supplyRequest.ImplementersCompanyId);
                await InnerSupplyReqestStageLinkCreate(newStage.Id, supplyRequest.Id);
            }
            else
            {
                throw new Exception("Этап/Поставка не найдена или попытка прикрепить счет к неподходящему этапу");
            }
        }

        public async Task SupplyRequestAttachPay(SupplyRequestAttachPay dto)
        {
            var spsLink = await _context.SupplyRequestLinks.FirstOrDefaultAsync(x => x.SuppluStageId == dto.StageId);
            var supplyRequest = await _context.SupplyRequests.FirstOrDefaultAsync(x => x.Id == spsLink.SuppluRequestId);
            if (supplyRequest != null && supplyRequest.CurrentStatus == "Счет получен")
            {
                supplyRequest.CurrentImplementerId= dto.NextImplementerId;
                supplyRequest.ImplementersCompanyId = dto.NextImplementerCompanyId;
                supplyRequest.CurrentStatus = $"Счет #{supplyRequest.ExpenseNumber} оплачен.";
                supplyRequest.IsPayed= true;
                _context.SupplyRequests.Attach(supplyRequest);
                await _context.SaveChangesAsync();
                var newStage = await InnerCompleteStage(dto.StageId, $"Счет #{supplyRequest.ExpenseNumber} оплачен. Ожидается поставка на склад.", supplyRequest.CurrentImplementerId, supplyRequest.ImplementersCompanyId);
                await InnerSupplyReqestStageLinkCreate(newStage.Id, supplyRequest.Id);
            }
            else
            {
                throw new Exception("Этап/Поставка не найдена или попытка прикрепить счет к неподходящему этапу");
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
            var spsLink = await _context.SupplyRequestLinks.FirstOrDefaultAsync(x => x.SuppluStageId == dto.StageId);
            var supplyRequest = await _context.SupplyRequests.FirstOrDefaultAsync(x => x.Id == spsLink.SuppluRequestId);
            if (supplyRequest != null)
            {
                supplyRequest.CurrentStatus = "Прибыло на склад";
                supplyRequest.CurrentImplementerId = dto.NextImplementerId;
                supplyRequest.ImplementersCompanyId = dto.NextImplementerCompanyId;
                _context.SupplyRequests.Attach(supplyRequest);
                await _context.SaveChangesAsync();
                var newStage = await InnerCompleteStage(dto.StageId, $"Материал: {supplyRequest.ProductName} в количестве {supplyRequest.RealCount} прибыл на склад. Осущестлвяется поставка на объект", supplyRequest.CurrentImplementerId, supplyRequest.ImplementersCompanyId);
                await InnerSupplyReqestStageLinkCreate(newStage.Id, supplyRequest.Id);
            }
            else
            {
                throw new Exception("Этап/Поставка не найдена или попытка прикрепить счет к неподходящему этапу");
            }
        }

        public async Task ConfirmSupplyStage(CompleteSupplyStageDTO dto)
        {
            var spsLink = await _context.SupplyRequestLinks.FirstOrDefaultAsync(x => x.SuppluStageId == dto.SupplyStageId);
            var supplyRequest = await _context.SupplyRequests.FirstOrDefaultAsync(x => x.Id == spsLink.SuppluRequestId);
            if (supplyRequest != null)
            {
                supplyRequest.CurrentImplementerId = dto.ImplementerId;
                supplyRequest.ImplementersCompanyId = dto.ImplementersCompanyId;
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
            var spsLink = await _context.SupplyRequestLinks.FirstOrDefaultAsync(x => x.SuppluStageId == dto.SupplyStageId);
            var supplyRequest = await _context.SupplyRequests.FirstOrDefaultAsync(x => x.Id == spsLink.SuppluRequestId);
            if (supplyRequest != null)
            {
                supplyRequest.CurrentStatus = "Canceled";
                _context.SupplyRequests.Attach(supplyRequest);
                var stage = await _context.RequestStages.FirstOrDefaultAsync(x => x.Id == dto.SupplyStageId);
                stage.CurrentStatus = "Canceled";stage.CancelDiscription = dto.CancelDiscription;
                _context.RequestStages.Attach(stage);
                await _context.SaveChangesAsync();
            }
        }
        private async Task InnerCompleteIncident(int incidentId)
        {
            var inc = _context.Incidents.FirstOrDefault(x => x.Id == incidentId);
            inc.Status = "Completed";
            inc.IsClosed = true;
            inc.ClosedAt = DateTime.Now.ToUniversalTime();
            _context.Incidents.Attach(inc);
            await _context.SaveChangesAsync();
        }

        public async Task<CommonRequestStage> GetLastServiceStage(int id)
        {
            var stage = await _context.RequestStages.Where(x => x.ServiceId == id).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if(stage != null)
                return stage;
            else
            {
                throw new Exception("Нет этапов связанных с данной заявкой");
            }
        }

        public async Task SupplyRequestWarehouseConfirm(SupplyRequestConfirmWarehouseDTO dto)
        {
            var spsLink = await _context.SupplyRequestLinks.FirstOrDefaultAsync(x => x.SuppluStageId == dto.StageId);
            var supplyRequest = await _context.SupplyRequests.FirstOrDefaultAsync(x => x.Id == spsLink.SuppluRequestId);
            if (supplyRequest != null)
            {
                supplyRequest.SupplierName = dto.SupplierName;
                supplyRequest.RealCount = dto.RealCount;
                supplyRequest.ExpenseNumber = "None";
                supplyRequest.CurrentImplementerId = dto.NextImplementerId;
                supplyRequest.ImplementersCompanyId = dto.NextImplementerCompanyId;
                supplyRequest.Expenses = 0;
                supplyRequest.IsPayed = false;
                supplyRequest.CurrentStatus = "Прибыло на склад";
                _context.SupplyRequests.Attach(supplyRequest);
                await _context.SaveChangesAsync();
                var newStage = await InnerCompleteStage(dto.StageId, $"Материал:{supplyRequest.ProductName} в наличии на складе предприятия. Осуществляется передача по заявке", supplyRequest.CurrentImplementerId, supplyRequest.ImplementersCompanyId);
                await InnerSupplyReqestStageLinkCreate(newStage.Id, supplyRequest.Id);

            }
            else
            {
                throw new Exception("Этап/Поставка не найдена или попытка прикрепить счет к неподходящему этапу");
            }
        }

        public async Task<ICollection<CommonServiceRequest>> GetAllIncidentRequestsByIncId(int id)
        {
            var incLinks = await _context.IncidentServiceLinks.Where(x => x.IncidentId == id).OrderBy(x => x.Id).ToListAsync();
            var incRequests = new List<CommonServiceRequest>();
            foreach (var inc in incLinks)
            {
                var buff = await _context.CommonRequests.FirstOrDefaultAsync(x => x.Id == inc.ServiceRequestId);
                if (buff != null)
                { 
                    incRequests.Add(buff); 
                }
            }
            if (incRequests.Count > 0)
            {
                return incRequests;
            }
            else
            {
                throw new Exception("На найдено заявок связанных с данной аварией");
            }
        }

        public async Task InnerSupplyReqestStageLinkCreate(int stageIs, int supplyReqId)
        {
            var newLink = new SuppliRequestStageLink()
            {
                SuppluRequestId = supplyReqId,
                SuppluStageId = stageIs
            };
            _context.SupplyRequestLinks.Add(newLink);
            await _context.SaveChangesAsync();
        }
    }
}
