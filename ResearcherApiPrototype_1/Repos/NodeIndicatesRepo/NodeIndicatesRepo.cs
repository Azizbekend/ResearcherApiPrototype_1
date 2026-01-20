using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1.DTOs.HardwaresDTOs;
using ResearcherApiPrototype_1.DTOs.NodesDTOs;
using ResearcherApiPrototype_1.Models;
using System.Globalization;
using static NpgsqlTypes.NpgsqlTsQuery;

namespace ResearcherApiPrototype_1.Repos.NodeIndicatesRepo
{
    public class NodeIndicatesRepo : INodeIndicatesRepo
    {
        private readonly AppDbContext _appDbContext;

        public NodeIndicatesRepo(AppDbContext context)
        {
            _appDbContext = context;
        }

        public async Task<NodeIndicates> CreateIndecatesAsync(string indicates, int nodeInfo)
        {
            //var nodeIndecates = new NodeIndicates(indicates, nodeInfo);
            //_appDbContext.NodesIndicates.Add(nodeIndecates);
            //await _appDbContext.SaveChangesAsync();
            //return nodeIndecates;
            throw new NotImplementedException();
        }

        public async Task CreateRangeIndecatesAsync(ICollection<NodeIndicates> coll)
        {
            _appDbContext.NodesIndicates.AddRange(coll);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<NodeIndecatesGroupResponseDTO> GetIndicatesByList(List<int> nodeInfos)
        {
            NodeIndecatesGroupResponseDTO dto = new NodeIndecatesGroupResponseDTO();
            //List<string> plcNodes = new List<string>();
            foreach (var node in nodeInfos)
            {          
                var a = await _appDbContext.Nodes.FirstOrDefaultAsync(x => x.Id == node);
                if (a != null && a.PlcNodeId != " ")
                {
                    var b = await GetIndicatesByPlcNodeIdAsync(a.PlcNodeId);
                    dto.indecatesGroup.Add(node, b.Indicates);
                    
                }
            }
            return dto;
        }

        public async Task<ICollection<NodeIndicates>> GetIndicatesByNodeIdAsync(int nodeId)
        {
            //return await _appDbContext.NodesIndicates
            //    .Include(ni => ni.NodeInfo)
            //    .ThenInclude(h => h.HardwareInfo)
            //    .ThenInclude(cb => cb.ControlBlock)
            //    .Where(ni => ni.NodeInfoId == nodeId)
            //    .ToListAsync();
            throw new NotFiniteNumberException();
        }

        public async Task<NodeIndicates> GetIndicatesByPlcNodeIdAsync(string plcNodeId)
        {
            NodeIndicates dto = new NodeIndicates();
            if (plcNodeId != null && plcNodeId != string.Empty && plcNodeId != " ")
                dto =  await _appDbContext.NodesIndicates
                    .Where(ni => ni.PlcNodeId == plcNodeId).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (dto != null)
            {

                return dto;
            }
            else
            {
                dto = new NodeIndicates();
                dto.Indicates = "No connection!";
                dto.TimeStamp = DateTime.Now;
                dto.PlcNodeId = plcNodeId;
                return dto;
            }
                
        }
        public async Task<NodeIndicates> GetByStrEnd(string end)
        {
            var indicates = await _appDbContext.NodesIndicates.Where(x => x.PlcNodeId.EndsWith(end)).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            return indicates;
        }
        public async Task<NodeIndicates> GetLastIndecatesByNodeIdAsync(int nodeId)
        {
            
            //return await _appDbContext.NodesIndicates
            //    .Include(ni => ni.NodeInfo)
            //    .ThenInclude(h => h.HardwareInfo)
            //    .ThenInclude(cb => cb.ControlBlock)
            //    .Where(ni => ni.NodeInfoId == nodeId).OrderByDescending(x => x.NodeInfoId).FirstAsync();
            throw new NotFiniteNumberException() ;
        }

        public async Task<ICollection<NodeIndicates>> GetGroupByStrEnd(string end)
        {
            var nodes = await _appDbContext.NodesIndicates.Where(x => x.PlcNodeId.EndsWith(end)).ToListAsync();
            return nodes;
        }

        public async Task<ForPasportDTO> GetBaseReadingsSha()
        {
            var culture = CultureInfo.GetCultureInfo("ru-RU");

           var dto = new ForPasportDTO();
           var el1 = await _appDbContext.NodesIndicates.Where(x => x.PlcNodeId.EndsWith("meter98_pwr_total")).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
           var el2 = await _appDbContext.NodesIndicates.Where(x => x.PlcNodeId.EndsWith("meter104_pwr_total")).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
           var water1 = await _appDbContext.NodesIndicates.Where(x => x.PlcNodeId.EndsWith("waterMeter1_counter")).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
           var water2 = await _appDbContext.NodesIndicates.Where(x => x.PlcNodeId.EndsWith("waterMeter2_counter")).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
           var hourP = await _appDbContext.NodesIndicates.Where(x => x.PlcNodeId.EndsWith("HMI_AI_FQIR1.hPV")).OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            dto.HourEfficiency = hourP.Indicates;
            double.TryParse(el1.Indicates, NumberStyles.Any, culture, out var el1Num);
            double.TryParse(el2.Indicates, NumberStyles.Any, culture, out var el2Num);
            double.TryParse(water1.Indicates, NumberStyles.Any, culture, out var wat1Num);
            double.TryParse(water2.Indicates, NumberStyles.Any, culture, out var wat2Num);

            dto.ElectroConsumption = Math.Round((el1Num + el2Num) / 1000, 2);
            dto.WaterConsumption = Math.Round((wat1Num + wat2Num) / 1000, 2);

            var list = await _appDbContext.NodesIndicates.Where(x => x.PlcNodeId.EndsWith("HMI_AI_FQIR1.hPV") && x.TimeStamp == DateTime.Today.AddDays(-1).ToUniversalTime()).ToListAsync();
            if (list.Count > 0)
            {
                double buff = 0;
                foreach (var item in list)
                {
                    double.TryParse(item.Indicates, NumberStyles.Any, culture, out var buff2);
                    buff += buff2;
                }

                dto.DayEfficiency = (buff / list.Count).ToString();
            }
            else
                dto.DayEfficiency = "0";
            return dto;

        }

        public async Task<ICollection<NodeIndicates>> GetStatusAllIndecates(GetHardwareLogDTO dto)
        {
            var hardwareStatusNode = await _appDbContext.Nodes.FirstOrDefaultAsync(x => x.HardwareId == dto.HadrwareId && x.PlcNodeId.EndsWith("hStatus"));
            var list = await _appDbContext.NodesIndicates.Where(x => x.PlcNodeId == hardwareStatusNode.PlcNodeId && x.TimeStamp >= dto.Start && x.TimeStamp <= dto.End).OrderBy(x => x.Id).ToListAsync();
            if (list.Count > 0)
            {
                var buff = list[0].Indicates;
                var responseList = new List<NodeIndicates>();
                responseList.Add(list[0]);
                foreach (var item in list)
                {
                    if (item.Indicates != buff)
                    {
                        responseList.Add(item);
                        buff = item.Indicates;
                    }
                }
                return responseList;
            }
            else
            {
                throw new Exception("Not found any indicates with current parameters!");
            }
        }
    }
}
