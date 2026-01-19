using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.DTOs.ObjectDTOs;
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

        public async Task<AttachCompanyToObjectDTO> AttachCompany(AttachCompanyToObjectDTO dto)
        {
            var baselink = _appDbContext.ObjectCompanyLinks.FirstOrDefault(x => x.CompanyId == dto.CompanyId && x.CompanyRole == dto.CompanyRole);
            if (baselink == null)
            {
                var link = new ObjectCompanyLink()
                {
                    CompanyName = dto.CompanyName,
                    CompanyId = dto.CompanyId,
                    ObjectId = dto.ObjectId,
                    CompanyRole = dto.CompanyRole
                };

                _appDbContext.ObjectCompanyLinks.Add(link);
                await _appDbContext.SaveChangesAsync();
                return dto;
            }
            else
            {
                throw new Exception($"Company {dto.CompanyName} with {dto.CompanyRole} role, already added to this object!");
            }
        }

        public async Task<AttachUserToObjectLinkDTO> AttachUser(AttachUserToObjectLinkDTO dto)
        {
            var baselink = await _appDbContext.UserObjectComandLinks.FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.ObjectCompanyLinkId == dto.ObjectCompanyLinkId);
            if (baselink == null)
            {
                var link = new UserObjectCompanyLink()
                {
                    UserId = dto.UserId,
                    ObjectCompanyLinkId = dto.ObjectCompanyLinkId
                };
                _appDbContext.UserObjectComandLinks.Add(link);  
                await _appDbContext.SaveChangesAsync();
                return dto;
            }
            else
            {
                throw new Exception($"User already added!");
            }
        }

        public async Task<StaticObjectInfo> Create(StaticObjectInfo dto)
        {
            var passport = new StaticObjectInfo()
            {
                Name = dto.Name,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Adress = dto.Adress,
                OperatingOrganization = dto.OperatingOrganization,
                CustomerName = dto.CustomerName,
                GeneralContractorName = dto.GeneralContractorName,
                ProjectEfficiency = dto.ProjectEfficiency,
                FileId = dto.FileId
            };
            //_appDbContext.StaticObjectInfos.Attach(passport);
            _appDbContext.StaticObjectInfos.Add(passport);     
            await _appDbContext.SaveChangesAsync();
            return passport;

        }

        public Task CreateAccesses(CreateObjectAccessesDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<StaticObjectInfo>> GetAll()
        {
            return await _appDbContext.StaticObjectInfos.OrderBy(x => x.Name). ToListAsync();
        }

        public async Task<ICollection<ObjectCompanyLink>> GetObjectCompanies(int id)
        {
            return await _appDbContext.ObjectCompanyLinks.Where(x => x.ObjectId == id).ToListAsync();
        }

        public async Task<StaticObjectInfo> GetSingleById(int id)
        {
            return await _appDbContext.StaticObjectInfos.FirstAsync(x => x.Id == id);
        }

        public async Task<ICollection<UserObjectCompanyLink>> GetUsersCompany(int id)
        {
            return await _appDbContext.UserObjectComandLinks.Where(x => x.UserId == id).ToListAsync();
        }

        public async Task<UpdateObjAccessesDTO> UpdateUsersAccesses(UpdateObjAccessesDTO dto)
        {
            var accesses = _appDbContext.UserObjectComandLinks.FirstOrDefault(x => x.UserId == dto.UserID && x.ObjectCompanyLinkId == dto.ObjectCompanyLinkId);
            if (accesses == null) { throw new Exception("No access to this Azizback!11"); }
            else
            {
                accesses.Is3DEnabled = dto.Is3DEnabled;
                accesses.IsCommandsEnabled = dto.IsCommandsEnabled;
                accesses.IsNodeInfosEnabled = dto.IsNodeInfosEnabled;   
                accesses.CanCreateIncidentServiseRequests = dto.CanCreateIncidentServiseRequests;
                accesses.CanCreateCommonServiceRequests = dto.CanCreateCommonServiceRequests;
                _appDbContext.UserObjectComandLinks.Attach(accesses);
                await _appDbContext.SaveChangesAsync();
                return dto;
            }

        }
    }
}
