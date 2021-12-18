using System.Reflection;

namespace Simulator.Engine.Contracts
{
    public class EventAgentCreated
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Extension { get; set; }
    }

    public class EventAgentStateChanged
    {
        public string Id { get; set; }
        public AgentState State { get; set; }
    }
}