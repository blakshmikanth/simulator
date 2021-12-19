using Akka.Actor;
using Microsoft.AspNetCore.Mvc;
using Simulator.Engine.Actors;
using Simulator.Engine.Contracts;
using Simulator.Engine.Directory;
using Simulator.Engine.Services;

namespace Simulator.Engine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentsController : ControllerBase
    {
        private readonly IStore _store;

        public AgentsController(IStore store)
        {
            _store = store;
        }
        
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_store.Agents);
        }
        
        
        [HttpPost]
        public IActionResult Post([FromBody] EventAgentCreated message)
        {
            var resource = new AgentResource()
            {
                Id = message.Id, FirstName = message.FirstName, LastName = message.LastName,
                Extension = message.Extension
            };
            SystemActors.Agents.Tell(AgentsActor.CreateAgentCreatedMessage(resource));
            return Accepted();
        }

        [HttpPost("change-state")]
        public IActionResult PostChangeAgentState([FromBody] RequestAgentStateChange message)
        {
            SystemActors.Agents.Tell(AgentsActor.CreateAgentStateChange(message));
            return Accepted();
        }

        [HttpGet("latest")]
        public IActionResult GetLatestStates()
        {
            SystemActors.Agents.Tell(AgentsActor.CreateRequestCurrentState());
            return Accepted();
        }
        
    }
}