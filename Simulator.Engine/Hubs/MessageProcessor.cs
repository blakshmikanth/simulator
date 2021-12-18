using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Simulator.Engine.Contracts;
using Simulator.Engine.Contracts.Messages;
using Simulator.Engine.Directory;

namespace Simulator.Engine.Hubs
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly IHubContext<MessageHub, IMessageClient> _hub;

        public MessageProcessor(IHubContext<MessageHub, IMessageClient> hub)
        {
            _hub = hub;
        }

        public async Task SendAsync(RequestSendMessage message)
        {
            await _hub.Clients.All.ReceiveMessage(MessageModel.Create(message));
        }
        
        public async Task SendAgentsAsync(IList<AgentResource> agents)
        {
            await _hub.Clients.All.ResponseAgents(agents);
        }

        public async Task SendAgentStateChanged(EventAgentStateChanged message)
        {
            await _hub.Clients.All.EventAgentStateChanged(message);
        }
    }
}