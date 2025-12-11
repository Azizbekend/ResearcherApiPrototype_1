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
            var dto = new NodeIndecatesGroupResponseDTO();

            var nodes = await _appDbContext.Nodes
                .Where(n => nodeInfos.Contains(n.Id))
                .ToListAsync();

            foreach (var node in nodes)
            {
                var b = await GetIndicatesByPlcNodeIdAsync(node.PlcNodeId);
                dto.indecatesGroup.Add(node.Id, b.Indicates);
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
