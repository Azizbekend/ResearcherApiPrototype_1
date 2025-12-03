using ResearcherApiPrototype_1.Models;
using Microsoft.EntityFrameworkCore;
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
            var nodeIndecates = new NodeIndicates(indicates, nodeInfo);
            _appDbContext.NodesIndicates.Add(nodeIndecates);
            await _appDbContext.SaveChangesAsync();
            return nodeIndecates;
        }

        public async Task CreateRangeIndecatesAsync(ICollection<NodeIndicates> coll)
        {
            _appDbContext.NodesIndicates.AddRange(coll);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<NodeIndicates>> GetIndicatesByNodeIdAsync(int nodeId)
        {
            return await _appDbContext.NodesIndicates
                .Include(ni => ni.NodeInfo)
                .ThenInclude(h => h.HardwareInfo)
                .ThenInclude(cb => cb.ControlBlock)
                .Where(ni => ni.NodeInfoId == nodeId)
                .ToListAsync();
        }

        public async Task<ICollection<NodeIndicates>> GetIndicatesByPlcNodeIdAsync(string plcNodeId)
        {
            return await _appDbContext.NodesIndicates
                .Include(ni => ni.NodeInfo)
                .ThenInclude(h => h.HardwareInfo)
                .ThenInclude(cb => cb.ControlBlock)
                .Where(ni => ni.NodeInfo.PlcNodeId == plcNodeId)
                .ToListAsync();
        }

        public async Task<NodeIndicates> GetLastIndecatesByNodeIdAsync(int nodeId)
        {
            return await _appDbContext.NodesIndicates
                //.Include(ni => ni.NodeInfo)
                //.ThenInclude(h => h.HardwareInfo)
                //.ThenInclude(cb => cb.ControlBlock)
                .Where(ni => ni.NodeInfoId == nodeId).OrderByDescending(x => x.NodeInfoId).FirstAsync();

        }
    }
}
