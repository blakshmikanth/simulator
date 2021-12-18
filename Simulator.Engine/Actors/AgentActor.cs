using Akka.Actor;
using Microsoft.Extensions.Logging;
using Simulator.Engine.Contracts;
using Simulator.Engine.Directory;
using Simulator.Engine.Services;

namespace Simulator.Engine.Actors
{
    public class AgentActor : ReceiveActor
    {
        private readonly ILogger<AgentActor> _logger;
        private readonly IActorRef _logActor = SystemActors.Logger;
        
        private string Id { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Extension { get; set; }
        private AgentState State { get; set; }

        public AgentActor(ILogger<AgentActor> logger)
        {
            _logger = logger;
            Receive<EventAgentCreated>(EventAgentCreatedHandler);
            Receive<RequestAgentStateChange>(RequestAgentStateChangeHandler);
        }
        
        private void EventAgentCreatedHandler(EventAgentCreated message)
        {
            Id = message.Id;
            FirstName = message.FirstName;
            LastName = message.LastName;
            Extension = message.Extension;
            State = AgentState.Unknown;
            _logActor.Tell(LoggerActor.CreateEventAgentStateChanged(Id, State));
        }

        private void RequestAgentStateChangeHandler(RequestAgentStateChange message)
        {
            State = message.State;
            _logActor.Tell(LoggerActor.CreateEventAgentStateChanged(Id, State));
        }
        
        protected override void Unhandled(object message)
        {
            _logger.LogError($"Unexpected message arrived : {message}");
           _logActor.Tell(message.ToString());
        }
        
        public static EventAgentCreated CreateAgentCreatedMessage(AgentResource resource) => new EventAgentCreated()
        {
            Id = resource.Id,
            FirstName = resource.FirstName,
            LastName = resource.LastName,
            Extension = resource.LastName
        };

    }
}