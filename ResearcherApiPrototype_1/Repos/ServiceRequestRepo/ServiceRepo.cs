using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1.DTOs.CommonServisesDTOs;
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
                _context.RequestStages.Attach(stage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CompleteRequest(CompleteCancelRequestME_DTO dto)
        {
            var check = await _context.RequestStages.FirstOrDefaultAsync(x => x.ServiceId == dto.RequestId && x.CurrentStatus != "Completed");
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

        public async Task CompleteStage(CompleteStageDTO dto)
        {
            var stage = await _context.RequestStages.FirstOrDefaultAsync(x => x.Id == dto.StageId);
            if(stage != null)
            {
                stage.CurrentStatus = "Completed";
                _context.RequestStages.Attach(stage);
                var newStage = new CommonRequestStage()
                {
                    ServiceId = stage.ServiceId,
                    CreatorId = stage.ImplementerId,
                    ImplementerId = stage.CreatorId,
                    StageType = stage.StageType,
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

        public async Task<CommonRequestStage> CreateRequestStage(CreateStageME_DTO dto)
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

        public async Task<ICollection<CommonRequestStage>> GetRequestStages(int id)
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
    }
}
