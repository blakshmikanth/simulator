using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.DependencyInjection;
using Microsoft.Extensions.Logging;
using Simulator.Engine.Contracts;
using Simulator.Engine.Contracts.Messages;
using Simulator.Engine.Directory;
using Simulator.Engine.Services;

namespace Simulator.Engine.Actors
{
    public class AgentsActor: ReceiveActor
    {
        private readonly ILogger<AgentsActor> _logger;
        private readonly IDictionary<string, IActorRef> _agents = new Dictionary<string, IActorRef>();

        public AgentsActor(ILogger<AgentsActor> logger)
        {
            _logger = logger;
            Receive<EventAgentCreated>(EventAgentCreatedHandler);
            Receive<RequestAgentStateChange>(RequestAgentStateChangeHandler);
        }

        private void EventAgentCreatedHandler(EventAgentCreated message)
        {
            // agent exists in the collection
            if (_agents.ContainsKey(message.Id)) return;

            var props = DependencyResolver.For(Context.System).Props<AgentActor>();
            var actorRef = Context.System.ActorOf(props, message.Id);
            _agents.Add(message.Id, actorRef);
            
            // Forward the message so that agent can initialize itself
            actorRef.Forward(message);
            
            SystemActors.Logger.Tell(new RequestSendMessage()
            {
                Text = $"Agent actor with {actorRef.Path.Name} created successfully - {actorRef.Path}"
            });
        }

        private void RequestAgentStateChangeHandler(RequestAgentStateChange message)
        {
            // if agent doesn't exist in the collection, ignore this message
            if (!_agents.ContainsKey(message.AgentId)) return;
            
            // Forward the message to the agent to change status
            _agents[message.AgentId].Forward(message);
            
        }
   
        protected override void Unhandled(object message)
        {
            _logger.LogError($"Unexpected message arrived : {message}");
            SystemActors.Logger.Tell(message.ToString());
        }
   
        public static EventAgentCreated CreateAgentCreatedMessage(AgentResource resource) => new EventAgentCreated()
        {
            Id = resource.Id,
            FirstName = resource.FirstName,
            LastName = resource.LastName,
            Extension = resource.LastName
        };

        /// <summary>
        /// Dummy method to enforce that we are following standard practice
        /// </summary>
        /// <param name="message"><see cref="RequestAgentStateChange"/></param>
        /// <returns></returns>
        public static RequestAgentStateChange CreateAgentStateChange(RequestAgentStateChange message) => message;
    }
}