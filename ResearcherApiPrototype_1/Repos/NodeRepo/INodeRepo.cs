using ResearcherApiPrototype_1.DTOs.NodesDTOs;
using ResearcherApiPrototype_1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResearcherApiPrototype_1.Repos.NodeRepo
{
    public interface INodeRepo
    {
        Task<NodeInfo> CreateInfoNodeAsync(string name, string plcNodeId, string mesurement, int hardwareId);
        Task CreateMassInfoNodeAsync(MassNodeCreateDTO dto);
        Task CreateMassCommandNodeAsync(MassNodeCreateDTO dto);
        Task<NodeInfo> CreateCommandNodeAsync(string name, string plcNodeId, string mesurement, bool isValue, int hardwareId);
        Task<ICollection<NodeInfo>> GetAllNodesAsync();
        Task<ICollection<NodeInfo>> GetAllInfoNodesByHardwareId(int hardwareId);
        Task<ICollection<NodeInfo>> GetAllCommandNodesByHardwareId(int hardwareId);
        Task<ICollection<NodeInfo>> GetHardwareIncidentsNodes(int hardwareId);
        Task<ICollection<NodeInfo>> GetHardwaresAllIncidentsNodes(int  hardId);
        Task <string> IsCommonIncident(int hardwareId);
        Task<ICollection<NodeInfoIncidentDTO>> CheckIncidents(int hardwareId);
        Task<NodeInfo> UpdateNode(NodeUpdateDTO dto);
        Task DeleteInfo(int id);
    }
}
