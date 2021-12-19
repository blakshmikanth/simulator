namespace Simulator.Engine.Contracts
{
    public class RequestAgentStateChange : BaseRequest
    {
        public string AgentId { get; set; }
        public AgentState State { get; set; }
    }
}