using System.Collections.Generic;
using Akka.Actor;
using Akka.DependencyInjection;
using Microsoft.Extensions.Logging;
using Processor.Messages;
using Processor.Resources;

namespace Processor.Actors
{
    public class AgentsActor : ReceiveActor
    {
        private readonly ILogger<AgentsActor> _logger;
        private readonly IDictionary<string, IActorRef> _agents = new Dictionary<string, IActorRef>();

        public AgentsActor(ILogger<AgentsActor> logger)
        {
            _logger = logger;
            Receive<Agent>(AgentHandler);
            Receive<RequestInitiateSimulation>(RequestInitiateSimulationHandler);
        }

        private void AgentHandler(Agent message)
        {
            if (_agents.ContainsKey(message.Id)) return;

            var props = DependencyResolver.For(Context.System).Props<AgentActor>();
            var actorRef = Context.System.ActorOf(props, message.Id);
            
            _agents.Add(message.Id, actorRef);
            
            //Forward the message to initialize agent
            actorRef.Forward(message);
            
            _logger.LogInformation($"Agent actor with {actorRef.Path.Name} created successfully = {actorRef.Path}");
        }

        private void RequestInitiateSimulationHandler(RequestInitiateSimulation message)
        {
            foreach (var agentRef in _agents.Values)
            {
                agentRef.Forward(message);
            }
        }
    }
}