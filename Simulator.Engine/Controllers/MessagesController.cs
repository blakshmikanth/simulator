using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Simulator.Engine.Contracts.Messages;
using Simulator.Engine.Hubs;

namespace Simulator.Engine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageProcessor _processor;
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(IMessageProcessor processor, ILogger<MessagesController> logger)
        {
            _processor = processor;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(RequestSendMessage message)
        {
            await _processor.SendAsync(message);
            return Ok();
        }
    }
}