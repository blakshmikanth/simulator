using Microsoft.AspNetCore.SignalR;

namespace Simulator.Engine.Hubs
{
    public class MessageHub: Hub<IMessageClient>
    {
    }
}