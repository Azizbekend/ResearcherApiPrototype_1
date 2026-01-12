using ResearcherApiPrototype_1.DTOs.NodesDTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.NodeIndicatesRepo
{
    public interface INodeIndicatesRepo
    {
        Task<NodeIndicates> CreateIndecatesAsync(string indicates, int nideInfoId);
        Task CreateRangeIndecatesAsync(ICollection<NodeIndicates> coll);
        Task <ICollection<NodeIndicates>> GetIndicatesByNodeIdAsync(int nodeId);
        Task<NodeIndicates> GetLastIndecatesByNodeIdAsync(int nodeId);
        Task <NodeIndicates> GetIndicatesByPlcNodeIdAsync(string plcNodeId);
        Task<NodeIndecatesGroupResponseDTO> GetIndicatesByList(List<int> nodeids);
        Task<NodeIndicates> GetByStrEnd(string end);
        Task<ICollection<NodeIndicates>> GetGroupByStrEnd(string end);
        Task<ForPasportDTO> GetBaseReadingsSha();




    }
}
