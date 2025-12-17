using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1.DTOs.BaseCreateDTOs;
using ResearcherApiPrototype_1.Models;
using System.Collections.Immutable;

namespace ResearcherApiPrototype_1.Repos.MaintenanceRepo
{
    public class MaintenanceRepo : IMaintenanceRepo
    {
        private readonly AppDbContext _appDbContext;

        public MaintenanceRepo(AppDbContext context)
        {
            _appDbContext = context;
        }
        public async Task CompleteMaintenanceRequest(int id)
        {
            var mRequest = _appDbContext.MaintenanceRequests.FirstOrDefault(x => x.Id == id);
            if (mRequest != null)
            {
                var record = new MaintenanceHistory
                {
                    CompletedMaintenanceDate = DateTime.Now,
                    SheduleMaintenanceDate = mRequest.NextMaintenanceDate,    
                    MaintenanceRequestId = mRequest.Id
                };
                _appDbContext.MaintenanceHistory.Add(record);
                mRequest.NextMaintenanceDate = DateTime.Now.AddHours(mRequest.Period);
                //var history = new MaintenanceHistory { CompletedMaintenanceDate = DateTime.Now, MaintenanceId = mRequest.Id};
                _appDbContext.MaintenanceRequests.Attach(mRequest);
                await _appDbContext.SaveChangesAsync();
            }
            else throw new Exception("Maintenance request not found!");
        }
        
        public async Task<MaintenanceRequest> Create(MaintanceCreateDTO request)
        {
            var hw = _appDbContext.Hardwares.First(x => x.Id == request.HardwareId);

            var mRequest = new MaintenanceRequest
            {
                Title = request.Title,
                Discription = request.Discription,
                NextMaintenanceDate = hw.ActivatedAt.AddHours(request.Period).ToLocalTime(),
                Period = request.Period,
                HardwareId = request.HardwareId
            };
            _appDbContext.MaintenanceRequests.Add(mRequest);
            await _appDbContext.SaveChangesAsync();
            return mRequest;

        }

        public async Task<ICollection<MaintenanceRequest>> GetHardwareMaintenanceRequests(int id)
        {
            return await _appDbContext.MaintenanceRequests.Include(x => x.HardwareId)
                .Where(x  => x.HardwareId == id)
                .ToListAsync();
        }

        public async Task<ICollection<MaintenanceHistory>> GetHistoryCompleteRecords(int requestId)
        {
            return await _appDbContext.MaintenanceHistory.Where(x => x.MaintenanceRequestId == requestId).OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<ICollection<MaintenanceRequest>> GetTodayRequests(int requestId)
        {
            return await _appDbContext.MaintenanceRequests
                .Where(x => x.HardwareId == requestId && x.NextMaintenanceDate.Date <= DateTime.Now.Date)
                .ToListAsync();
        }
        public async Task<ICollection<MaintenanceRequest>> GetNextWeekRequests(int requestId)
        {
            return await _appDbContext.MaintenanceRequests
                .Where(x => x.HardwareId == requestId && x.NextMaintenanceDate.Date > DateTime.Now.Date && x.NextMaintenanceDate <= DateTime.Now.AddDays(7))
                .ToListAsync();
        }

        public async Task<ICollection<MaitenanceHistoryGetManyDTO>> GetHardwareAllHistory(int id)
        {
            List<MaitenanceHistoryGetManyDTO> list = new List<MaitenanceHistoryGetManyDTO>(); 
            var requestIds = await _appDbContext.MaintenanceRequests.Where(x=> x.HardwareId == id).ToListAsync();
            foreach ( var requestId in requestIds )
            {
                var title = requestId.Title;
                var records = await _appDbContext.MaintenanceHistory.Where(x => x.MaintenanceRequestId== requestId.Id).ToListAsync();
                list.Add(new MaitenanceHistoryGetManyDTO
                {
                    Title = title,
                    RecordsList = records
                });
            }
            return list;
        }

        public async  Task<ICollection<MaintenanceHistoryFilteredDTO>> GetHardwareAllHistoryFilteref(int id)
        {
            List<MaintenanceHistoryFilteredDTO> list = new List<MaintenanceHistoryFilteredDTO>();
            var requestIds = await _appDbContext.MaintenanceRequests.Where(x => x.HardwareId == id).ToListAsync();
            foreach (var requestId in requestIds)
            {
                var title = requestId.Title;
                var records = await _appDbContext.MaintenanceHistory.Where(x => x.MaintenanceRequestId == requestId.Id).ToListAsync();
                foreach(var rec in records)
                {
                    list.Add(new MaintenanceHistoryFilteredDTO
                    {
                        Title = title,
                        CompletedMaintenanceDate = rec.CompletedMaintenanceDate,
                        SheduleMaintenanceDate = rec.SheduleMaintenanceDate
                    });

                }
                list = list.OrderByDescending(x => x.CompletedMaintenanceDate).ToList();
            }
            return list;
            
        }
    }
}
