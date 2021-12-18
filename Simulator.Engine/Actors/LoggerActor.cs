using System.Collections.Generic;
using Akka.Actor;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Simulator.Engine.Contracts;
using Simulator.Engine.Contracts.Messages;
using Simulator.Engine.Hubs;

namespace Simulator.Engine.Actors
{
    public class LoggerActor: ReceiveActor
    {
        private readonly IMessageProcessor _processor;
        private readonly ILogger<LoggerActor> _logger;

        public LoggerActor(IMessageProcessor processor, ILogger<LoggerActor> logger)
        {
            _processor = processor;
            _logger = logger;
            Receive<RequestSendMessage>(LogMessageHandler);
            Receive<EventAgentStateChanged>(EventAgentStateChangedHandler);
        }
        private void LogMessageHandler(RequestSendMessage message)
        {
            _processor.SendAsync(message);
        }
        
        private void EventAgentStateChangedHandler(EventAgentStateChanged message)
        {
            _processor.SendAgentStateChanged(message);
        }

        protected override void Unhandled(object message)
        {
            _logger.LogError($"Unexpected message received - { message}");
        }

        public static EventAgentStateChanged CreateEventAgentStateChanged(string id, AgentState state) =>
            new EventAgentStateChanged()
            {
                Id = id,
                State = state
            };

    }
}