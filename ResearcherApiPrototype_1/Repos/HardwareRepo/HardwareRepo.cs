using ResearcherApiPrototype_1.Models;
using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1.DTOs.HardwaresDTOs;
using ResearcherApiPrototype_1.DTOs.BaseCreateDTOs;

namespace ResearcherApiPrototype_1.Repos.HardwareRepo
{
    public class HardwareRepo : IHardwareRepo
    {
        private readonly AppDbContext _appDbContext;

        public HardwareRepo(AppDbContext context)
        {
            _appDbContext = context;
        }

        public async Task CreateCommandEvent(CreateCommandEventDTO dto)
        {
            var hardwareEvent = new HardwareEvent
            {
                UserId = dto.UserId,
                HardwareId = dto.HardwareId,
                Discription = $"Пользователь изменил значение {dto.NodeName} на {dto.Indicates}"
            };
            _appDbContext.EventsJournal.Add(hardwareEvent);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<HardwareInfo> CreateHardwareAsync(HardwareCreateDTO dto)
        {
            var hardware = new HardwareInfo
            {
                Name = dto.Name,
                Model = dto.Model,
                Category = dto.Category,
                DeveloperName = dto.DeveloperName,
                SupplierName = dto.SupplierName,
                Position = dto.Position,
                PhotoName = dto.PhotoName,
                OpcDescription = dto.OpcDescription,
                ControlBlockId = dto.ControlBlockId,
                FileId = dto.FileId
            };
            _appDbContext.Hardwares.Add(hardware);
            await _appDbContext.SaveChangesAsync();

            return hardware;
        }

        public Task CreateNodeEvent(CreateHardwareNodeEventDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<HardwareInfo>> GetAllHardwaresAsync()
        {
            return await _appDbContext.Hardwares
            .Include(h => h.ControlBlock).OrderBy(x => x.Id)
            .ToListAsync();
        }

        public async Task<HardwareInfo> GetHardwareByIdAsync(int hardwareId)
        {

            return await _appDbContext.Hardwares
                .Include(h => h.ControlBlock)
                .Where(h => h.Id == hardwareId)
                .FirstAsync();
        }

        public async Task<ICollection<HardwareEvent>> GetHardwareEventLogs(int hardwareId, DateTime start, DateTime end)
        {
            var list = await _appDbContext.EventsJournal.Where(x => x.HardwareId == hardwareId && x.TimeStamp >= start && x.TimeStamp <= end).OrderBy(x => x.Id).ToListAsync();
            return list;
        }

        public async Task<ICollection<HardwareInfo>> GetHardwaresByControlBlockIdAsync(int controlBlockId)
        {
            
            return await _appDbContext.Hardwares
                .Include (h => h.ControlBlock)
                .Where(h=>h.ControlBlockId == controlBlockId)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<HardwareStatusDTO> GetHardwaresStatusByIdAsync(BaseSendListDTO dto)
        {
            HardwareStatusDTO dictionary = new HardwareStatusDTO();
            var listNodes = new List<NodeInfo>();
            foreach(var item in dto.Ids)
            {
                dictionary.HardwareId = item;
                var node = await _appDbContext.Nodes.Where(x => x.HardwareId == item && x.PlcNodeId.EndsWith("hStatus")).FirstOrDefaultAsync();
                if (node != null)
                {
                    listNodes.Add(node);
                }
            }
            foreach (var node in listNodes)
            {
                var indicates = await _appDbContext.NodesIndicates.Where(x => x.PlcNodeId == node.PlcNodeId).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                if(indicates  != null)
                {
                    dictionary.StatusDictionary.Add(node.Name, indicates.Indicates);
                }
            }
            return dictionary;
        }

        public async Task<ICollection<HardwareIncidentGroupDTO>> HadrdwareStatusCheck(BaseSendListDTO dto)
        {
            var checkedList = new List<HardwareIncidentGroupDTO>();
            foreach(var item in dto.Ids)
            {
                var hardwareNodes = await _appDbContext.Nodes.Where(n => n.HardwareId == item && (n.PlcNodeId.Trim().EndsWith("hAlmAi") || n.PlcNodeId.Trim().EndsWith("hAlmQF") || n.PlcNodeId.Trim().EndsWith("hAlmStator") ||
               n.PlcNodeId.Trim().EndsWith("hAlmVentQF") || n.PlcNodeId.Trim().EndsWith("hAlmVentCmd") || n.PlcNodeId.Trim().EndsWith("hAlmDisconnect") ||
               n.PlcNodeId.EndsWith("hAlmFC") || n.PlcNodeId.EndsWith("hAlmKonc") || n.PlcNodeId.EndsWith("hAlmCmd")
               || n.PlcNodeId.Trim().EndsWith("hAlmMoment") || n.PlcNodeId.Trim().EndsWith("hAlmExt") || n.PlcNodeId.Trim().EndsWith("hStatus"))).ToListAsync();
                if (hardwareNodes.Count == 0)
                {
                    var returnDTO = new HardwareIncidentGroupDTO();
                    returnDTO.HardwareId = item;
                    returnDTO.HardwareStatus = "Not found";
                    returnDTO.Incidents = "Not Found";
                    checkedList.Add(returnDTO);
                }
                else
                {
                    var returnDTO = new HardwareIncidentGroupDTO();
                    returnDTO.HardwareId = item;
                    foreach (var hardwareNode in hardwareNodes)
                    {

                        if (hardwareNode.PlcNodeId.EndsWith("hStatus"))
                        {
                            var status = await _appDbContext.NodesIndicates.Where(x => x.PlcNodeId == hardwareNode.PlcNodeId).OrderByDescending(x => x.Id).FirstAsync();
                            if(status.Indicates == "True")
                            {
                                returnDTO.HardwareStatus = "True";
                            }
                            else
                            {
                                if (status.Indicates == "False")
                                {
                                    returnDTO.HardwareStatus = "False";
                                }
                                else
                                    returnDTO.HardwareStatus = status.Indicates == "1" ? "True" : "False";
                            }

                        }
                        else
                        {
                            var check = await _appDbContext.NodesIndicates.FirstOrDefaultAsync(x => x.PlcNodeId == hardwareNode.PlcNodeId);
                            if (check != null)
                            {
                                var indicates = await _appDbContext.NodesIndicates.Where(x => x.PlcNodeId == hardwareNode.PlcNodeId).OrderByDescending(x => x.Id).FirstAsync();
                                if (indicates.Indicates == "True")
                                { 
                                    returnDTO.Incidents = indicates.Indicates;
                                    break;
                                }
                                else
                                    returnDTO.Incidents = indicates.Indicates;
                            }
                            else

                                returnDTO.Incidents = "Not Found";

                        }
                        
                    }
                    checkedList.Add(returnDTO);
                }
                
                //foreach(var hardwareNode in hardwareNodes)
                //{
                //    var alarm = await _appDbContext.NodesIndicates.FirstOrDefaultAsync(x => x.PlcNodeId == hardwareNode.PlcNodeId);
                //    var buff = await _appDbContext.NodesIndicates.Where(x => x.PlcNodeId == hardwareNode.PlcNodeId.Trim()).OrderByDescending(x => x.Id).FirstAsync();
                //    if (hardwareNode.PlcNodeId.Trim().EndsWith("hStatus"))
                //    {
                        
                //        returnDTO.HardwareStatus = buff.Indicates;
                //        if (alarm != null && alarm.Indicates == "True")
                //        {
                //            returnDTO.HardwareStatus = buff.Indicates;
                //            returnDTO.HardwareId = hardwareNode.HardwareId;
                //            returnDTO.Incidents = "True";
                //            checkedList.Add(returnDTO);
                //        }
                //        else
                //        {
                //            returnDTO.HardwareStatus = buff.Indicates;
                //            returnDTO.HardwareId = hardwareNode.HardwareId;
                //            returnDTO.Incidents = "False";
                //            checkedList.Add(returnDTO);
                //        }
                //    }        
                //    else
                //        if (alarm != null && alarm.Indicates == "True")
                //    {
                //        returnDTO.HardwareStatus = buff.Indicates;
                //        returnDTO.HardwareId = hardwareNode.HardwareId;
                //        returnDTO.Incidents = "True";
                //        checkedList.Add(returnDTO);
                //    }
                //    else
                //    {
                //        returnDTO.HardwareStatus = buff.Indicates;
                //        returnDTO.HardwareId = hardwareNode.HardwareId;
                //        returnDTO.Incidents = "False";
                //        checkedList.Add(returnDTO);
                //    }
                //}
                
            }
            return checkedList;
        }
       
        public async Task HardwareActivating(int id)
        {
            var hw = new HardwareInfo
            {
                Id = id,
                ActivatedAt = DateTime.Now.ToLocalTime()
            };
            _appDbContext.Hardwares.Attach(hw);
            _appDbContext.Entry(hw).Property(act => act.ActivatedAt).IsModified = true;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task HardwareDelete(int id)
        {
            var hi = await _appDbContext.Hardwares.FirstOrDefaultAsync(h => h.Id == id);
            _appDbContext.Hardwares.Remove(hi);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<NodeInfo>> HardwareIncidentsCheck(int id)
        {
           var list = new List<NodeInfo>();
           var nodes = await _appDbContext.Nodes.Where(x => x.HardwareId == id && (x.PlcNodeId.Trim().EndsWith("hAlmAi") || x.PlcNodeId.Trim().EndsWith("hAlmQF") || x.PlcNodeId.Trim().EndsWith("hAlmStator") ||
               x.PlcNodeId.Trim().EndsWith("hAlmVentQF") || x.PlcNodeId.Trim().EndsWith("hAlmVentCmd") || x.PlcNodeId.Trim().EndsWith("hAlmDisconnect") ||
               x.PlcNodeId.EndsWith("hAlmFC") || x.PlcNodeId.EndsWith("hAlmKonc") || x.PlcNodeId.EndsWith("hAlmCmd")
               || x.PlcNodeId.Trim().EndsWith("hAlmMoment") || x.PlcNodeId.Trim().EndsWith("hAlmExt"))).ToListAsync();
            foreach (var node in nodes)
            {
                var incident = await _appDbContext.NodesIndicates.Where(x => x.PlcNodeId == node.PlcNodeId).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                if(incident != null && incident.Indicates == "True")
                {
                    list.Add(node);
                }
            }
            return list;
        }

        public async Task<HardwareInfo> HardwareInfoUpdate(HardwareInfoUpdateDTO dto)
        {

            var hi = await _appDbContext.Hardwares.FirstOrDefaultAsync(x=>x.Id == dto.Id);
            if (hi != null)
            {
                hi.Name = dto.Name;
                hi.Model = dto.Model;
                hi.Category = dto.Category;
                hi.DeveloperName = dto.DeveloperName;
                hi.SupplierName = dto.SupplierName;
                hi.Position = dto.Position;
                hi.OpcDescription = dto.OpcDescription;
                hi.FileId = dto.FileId;
                _appDbContext.Hardwares.Attach(hi);
                await _appDbContext.SaveChangesAsync();
                return hi;
            }
            else
                throw new Exception("Not Found");

        
        }
    }
}
