using ResearcherApiPrototype_1.Models;
using ResearcherApiPrototype_1.Repos.NodeRepo;
using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1.DTOs.NodesDTOs;


namespace ResearcherApiPrototype_1.Repos.NodeRepo
{
    public class NodeRepo : INodeRepo
    {
        private readonly AppDbContext _appDbContext;

        public NodeRepo(AppDbContext context)
        {
            _appDbContext = context;
        }

        public async Task<NodeInfo> CreateCommandNodeAsync(string name, string plcNodeId, string mesurement, bool isValue, int hardwareId)
        {
            var node = new NodeInfo
            {
                Name = name,
                PlcNodeId = plcNodeId,
                Mesurement = mesurement,
                IsCommand = true,
                IsValue = isValue,
                HardwareId = hardwareId
            };
            _appDbContext.Nodes.Add(node);
            await _appDbContext.SaveChangesAsync();
            return node;
        }

        public async Task<NodeInfo> CreateInfoNodeAsync(string name, string plcNodeId, string mesurement,  int hardwareId)
        {
            var node = new NodeInfo
            {
                Name = name,
                PlcNodeId = plcNodeId,
                Mesurement = mesurement,
                HardwareId = hardwareId
            };         
                _appDbContext.Nodes.Add(node);
                await _appDbContext.SaveChangesAsync();
                return node;
        }



        public async Task<ICollection<NodeInfo>> GetAllNodesAsync()
        {
            return await _appDbContext.Nodes.Include(n => n.HardwareInfo).
                ThenInclude(n => n.ControlBlock).
                ToListAsync();        
        }

        public async Task<ICollection<NodeInfo>> GetAllInfoNodesByHardwareId(int hardwareId)
        {
            return await _appDbContext.Nodes
                .Include(n => n.HardwareInfo)
                .ThenInclude(h => h.ControlBlock)
                .Where(n => n.HardwareId == hardwareId && n.IsCommand == false )
                .OrderBy(n => n.Name)
                .ToListAsync();
        }

        public async Task<ICollection<NodeInfo>> GetAllCommandNodesByHardwareId(int hardwareId)
        {
            return await _appDbContext.Nodes
                .Include(n => n.HardwareInfo)
                .ThenInclude(h => h.ControlBlock)
                .Where(n => n.HardwareId == hardwareId && n.IsCommand == true)
                .OrderBy(n => n.Name)
                .ToListAsync();
        }

        public async Task CreateMassInfoNodeAsync(MassNodeCreateDTO dto)
        {
            foreach (var node in dto.Nodes)
            {
                var newnode = new NodeInfo()
                {
                    Name = node.Name,
                    PlcNodeId = node.PlcNodeId,
                    Mesurement = node.Mesurement,
                    IsCommand = false,
                    HardwareId = dto.HardwareId,
                    MinValue = node.MinValue,
                    MaxValue = node.MaxValue
                };
                _appDbContext.Nodes.Add(newnode);
            }
            await _appDbContext.SaveChangesAsync();
        }

        public async Task CreateMassCommandNodeAsync(MassNodeCreateDTO dto)
        {
            foreach (var node in dto.Nodes)
            {
                var newnode = new NodeInfo()
                {
                    Name = node.Name,
                    PlcNodeId = node.PlcNodeId,
                    Mesurement = node.Mesurement,
                    IsCommand = true,
                    IsValue = node.IsValue,
                    HardwareId = dto.HardwareId,
                    MinValue = node.MinValue,
                    MaxValue = node.MaxValue
                };
                _appDbContext.Nodes.Add(newnode);
            }
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<NodeInfo> UpdateNode(NodeUpdateDTO dto)
        {
            var node = await _appDbContext.Nodes.FirstOrDefaultAsync(x=>x.Id == dto.Id);
            if (node != null)
            {
                node.Name = dto.Name;
                node.IsValue = dto.IsValue;
                node.Mesurement = dto.Mesurement;
                node.PlcNodeId = dto.PlcNodeId;
                node.MinValue = dto.MinValue;
                node.MaxValue = dto.MaxValue;
                _appDbContext.Nodes.Attach(node);
                await _appDbContext.SaveChangesAsync();
                return node;
            }
            else
                throw new Exception("Not found!");
        }

        public async Task DeleteInfo(int id)
        {
            var ni = await _appDbContext.Nodes.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.Nodes.Remove(ni);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<NodeInfo>> GetHardwareIncidentsNodes(int hardwareId)
        {
            return await _appDbContext.Nodes
                .Where(x => x.HardwareId == hardwareId && x.PlcNodeId.EndsWith("hAlmCom"))
                .ToListAsync();
        }

        public async Task<ICollection<NodeInfo>> GetHardwaresAllIncidentsNodes(int hardId)
        {

            return await _appDbContext.Nodes
               .Include(n => n.HardwareInfo)
               .ThenInclude(h => h.ControlBlock)
               .Where(n => n.HardwareId == hardId && (n.PlcNodeId.EndsWith("hAlmAi") || n.PlcNodeId.EndsWith("hAlmQF") || n.PlcNodeId.EndsWith("hAlmStator") ||
               n.PlcNodeId.EndsWith("hAlmVentQF") || n.PlcNodeId.EndsWith("hAlmVentCmd") || n.PlcNodeId.EndsWith("hAlmDisconnect") ||
               n.PlcNodeId.EndsWith("hAlmFC") || n.PlcNodeId.EndsWith("hAlmKonc") || n.PlcNodeId.EndsWith("hAlmCmd")
               || n.PlcNodeId.EndsWith("hAlmMoment") || n.PlcNodeId.EndsWith("hAlmExt")))
               .OrderBy(n => n.Name)
               .ToListAsync();
        }

        public async Task<string> IsCommonIncident(int hardwareId)
        {
            var node = await _appDbContext.Nodes.FirstOrDefaultAsync(x => x.HardwareId == hardwareId && x.PlcNodeId.EndsWith("hAlmCom"));
            if (node != null && node.PlcNodeId.Length > 5)
            {
                var status = await _appDbContext.NodesIndicates.Where(x => x.PlcNodeId == node.PlcNodeId).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                if (status != null)
                    return status.Indicates;
                else
                    return "Not found!";
            }
            else
                return "Not found!";
        }

        public async Task<ICollection<NodeInfoIncidentDTO>> CheckIncidents(int hardwareId)
        {
            List<NodeInfoIncidentDTO> list = new List<NodeInfoIncidentDTO>();
            var nodes = await _appDbContext.Nodes
               .Include(n => n.HardwareInfo)
               .ThenInclude(h => h.ControlBlock)
               .Where(n => n.HardwareId == hardwareId && (n.PlcNodeId.EndsWith("hAlmAi") || n.PlcNodeId.EndsWith("hAlmQF") || n.PlcNodeId.EndsWith("hAlmStator") ||
               n.PlcNodeId.EndsWith("hAlmVentQF") || n.PlcNodeId.EndsWith("hAlmVentCmd") || n.PlcNodeId.EndsWith("hAlmDisconnect") ||
               n.PlcNodeId.EndsWith("hAlmFC") || n.PlcNodeId.EndsWith("hAlmKonc") || n.PlcNodeId.EndsWith("hAlmCmd")
               || n.PlcNodeId.EndsWith("hAlmMoment") || n.PlcNodeId.EndsWith("hAlmExt")))
               .OrderBy(n => n.Name)
               .ToListAsync();

            foreach ( var node in nodes)
            {
                var indicates = await _appDbContext.NodesIndicates.Where(x=> x.PlcNodeId == node.PlcNodeId).OrderByDescending(x=> x.Id).FirstOrDefaultAsync();
                if(indicates != null && indicates.Indicates == "True")
                {
                    var dto = new NodeInfoIncidentDTO
                    {
                        NodeId = node.Id,
                        NodeName = node.Name
                    };
                    list.Add(dto);
                }

            }
            return list;
        }

        public async Task<ICollection<NodeInfo>> GetNodeByStringEnd(string strEnd)
        {
            var node = await _appDbContext.Nodes.Where(x => x.PlcNodeId.EndsWith(strEnd)).ToListAsync();
            return node;
        }

        public async Task<NodeInfo> GetNodeById(int id)
        {
            return await _appDbContext.Nodes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<string> GetRemoteStatus(int hardwareId)
        {
            var node = _appDbContext.Nodes.FirstOrDefault(x => x.HardwareId == hardwareId && x.PlcNodeId.Trim().EndsWith("hRemote"));
            if (node == null)
                throw new Exception("Could not found RemoteControlNode for this Azizbeck!");
            else
                return node.PlcNodeId;
        }

        public async Task AttachLastValue(int nodeId, string strValue)
        {
            var node = await _appDbContext.Nodes.FirstOrDefaultAsync(x => x.Id == nodeId);
            node.LastValue = strValue;
            _appDbContext.Nodes.Attach(node);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
