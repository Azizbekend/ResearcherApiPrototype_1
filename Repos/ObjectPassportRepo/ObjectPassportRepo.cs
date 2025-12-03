using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.ObjectPassportRepo
{
    public class ObjectPassportRepo : IObjectPassportRepo
    {
        private readonly AppDbContext _appDbContext;

        public ObjectPassportRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<StaticObjectInfo> Create(StaticObjectInfo dto)
        {
            var passport = new StaticObjectInfo()
            {
                Adress = dto.Adress,
                OperatingOrganization = dto.OperatingOrganization,
                CustomerName = dto.CustomerName,
                GeneralContractorName = dto.GeneralContractorName,
                ProjectEfficiency = dto.ProjectEfficiency,
                PhotoName = dto.PhotoName
            };
            _appDbContext.StaticObjectInfos.Attach(passport);
            _appDbContext.StaticObjectInfos.Add(dto);     
            await _appDbContext.SaveChangesAsync();
            return passport;

        }

        public async Task<ICollection<StaticObjectInfo>> GetAll()
        {
            return await _appDbContext.StaticObjectInfos.OrderBy(p=>p.GeneralContractorName) .ToListAsync();
        }
    }
}
