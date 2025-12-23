using ComandSenderManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace ResearcherApiPrototype_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandController : ControllerBase
    {
        private readonly IComandSender _comandSender;

        public ComandController(IComandSender comandSender)
        {
            _comandSender = comandSender;
        }

        [HttpPost("send/command/string")]
        public async Task<IActionResult> SendComandString(string ip, string nodeId, string value)
        {
            var session = await _comandSender.CreateSession(ip);
           await _comandSender.SendComandString(session, nodeId, value);
            return Ok();
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
