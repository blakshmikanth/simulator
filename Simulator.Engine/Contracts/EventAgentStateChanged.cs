namespace Simulator.Engine.Contracts
{
    public class EventAgentStateChanged
    {
        public string Id { get; set; }
        public AgentState State { get; set; }
    }
}