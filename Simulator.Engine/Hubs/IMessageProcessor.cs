using System.Collections.Generic;
using System.Threading.Tasks;
using Simulator.Engine.Contracts;
using Simulator.Engine.Contracts.Messages;
using Simulator.Engine.Directory;

namespace Simulator.Engine.Hubs
{
    public interface IMessageProcessor
    {
        Task SendAsync(RequestSendMessage message);

        Task SendAgentsAsync(IList<AgentResource> agents);

        Task SendAgentStateChanged(EventAgentStateChanged message);
    }
}