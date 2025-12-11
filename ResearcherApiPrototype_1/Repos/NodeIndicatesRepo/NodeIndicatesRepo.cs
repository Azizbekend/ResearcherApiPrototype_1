using ResearcherApiPrototype_1.Models;
using Microsoft.EntityFrameworkCore;
using static NpgsqlTypes.NpgsqlTsQuery;
using ResearcherApiPrototype_1.DTOs;

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
                if (a != null)
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
            return await _appDbContext.NodesIndicates
                .Where(ni => ni.PlcNodeId == plcNodeId).OrderByDescending(x => x.Id).FirstAsync();
                
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

        


    }
}
