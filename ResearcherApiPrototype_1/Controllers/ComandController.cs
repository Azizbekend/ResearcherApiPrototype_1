using ComandSenderManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.Repos.ControlBlockRepo;
using ResearcherApiPrototype_1.Repos.HardwareRepo;
using ResearcherApiPrototype_1.Repos.NodeRepo;
using System.Collections.Specialized;

namespace ResearcherApiPrototype_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandController : ControllerBase
    {
        private readonly IComandSender _comandSender;
        private readonly INodeRepo _nodeRepo;
        private readonly IControlBlockRepo _controlBlockRepo;
        private readonly IHardwareRepo _hardwareRopo;



        public ComandController(IComandSender comandSender, INodeRepo nodeRepo, IControlBlockRepo controlBlockRepo, IHardwareRepo hardwareRopo)
        {
            _controlBlockRepo = controlBlockRepo;
            _hardwareRopo = hardwareRopo;
            _comandSender = comandSender;
            _nodeRepo = nodeRepo;

        }

        [HttpGet("check/remoteControlStatus")]
        public async Task<IActionResult> CheckRemoteStatus(int hardwareId)
        {
            var hardware = await _hardwareRopo.GetHardwareByIdAsync(hardwareId);
            var hRemote = await _nodeRepo.GetRemoteStatus(hardwareId);
            var cb = await _controlBlockRepo.GetByHardwareId(hardware.ControlBlockId);
            var session = await _comandSender.CreateSession(cb.PlcIpAdress);
            var status = await _comandSender.CheckRemoteControlStatus(session, hRemote);
            return Ok(status);
        } 

        [HttpGet("hardware/remoreControl/Activate")]
        public async Task<IActionResult> ActivateRemoteStatus(int hardwareId)
        {
            var hardware = await _hardwareRopo.GetHardwareByIdAsync(hardwareId);
            var hRemote = await _nodeRepo.GetRemoteStatus(hardwareId);
            var cb = await _controlBlockRepo.GetByHardwareId(hardware.ControlBlockId);
            var session = await _comandSender.CreateSession(cb.PlcIpAdress);
            var response = await _comandSender.SendComandBool(session, hRemote, true);
            return Ok(response);
        }

        [HttpGet("hardware/remoreControl/Deactivate")]
        public async Task<IActionResult> DeactivateRemoteStatus(int hardwareId)
        {
            var hardware = await _hardwareRopo.GetHardwareByIdAsync(hardwareId);
            var hRemote = await _nodeRepo.GetRemoteStatus(hardwareId);
            var cb = await _controlBlockRepo.GetByHardwareId(hardware.ControlBlockId);
            var session = await _comandSender.CreateSession(cb.PlcIpAdress);
            var response = await _comandSender.SendComandBool(session, hRemote, false);
            return Ok(response);
        }
        [HttpPost("send/command/string")]
        public async Task<IActionResult> SendComandString(int nodeId, string value)
        {
            var node = await _nodeRepo.GetNodeById(nodeId);
            var hw = await _hardwareRopo.GetHardwareByIdAsync(node.HardwareId);
            var controlBlock = await _controlBlockRepo.GetByHardwareId(hw.ControlBlockId);
            if (node.IsValue)
            {
                var val = await SendComandFloat(controlBlock.PlcIpAdress, node.PlcNodeId, float.Parse(value));
                await _nodeRepo.AttachLastValue(node.Id, value);
                return Ok(val);
            }
            else
                if (value == "true")
            {
                var val = await SendComandBool(controlBlock.PlcIpAdress, node.PlcNodeId, true);
                return Ok(val);
            }
            else
            {
                var val = await SendComandBool(controlBlock.PlcIpAdress, node.PlcNodeId, false);
                return Ok(val);
            }

        }

        [HttpPost("send/command/bool")]
        public async Task<IActionResult> SendComandBool(string ip, string nodeId, bool value)
        {
            var session = await _comandSender.CreateSession(ip);
            var node = await _comandSender.SendComandBool(session, nodeId, value);
            return Ok(node);
        }
        [HttpPost("send/command/float")]
        public async Task<IActionResult> SendComandFloat(string ip, string nodeId, float value)
        {
            var session = await _comandSender.CreateSession(ip);
            await _comandSender.SendComandFloat(session, nodeId, value);
            return Ok();
        }
    }
}
