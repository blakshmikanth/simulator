using System.Collections.Generic;
using System.Threading.Tasks;
using Simulator.Engine.Contracts;
using Simulator.Engine.Contracts.Messages;
using Simulator.Engine.Directory;

namespace Simulator.Engine.Hubs
{
    public interface IMessageClient
    {
        Task ReceiveMessage(MessageModel message);
        Task ResponseAgents(IList<AgentResource> message);
        
        Task EventAgentStateChanged(EventAgentStateChanged message);
    }
}