namespace Simulator.Engine.Contracts
{
    public class RequestAgentLogin : BaseRequest
    {
        public string AgentId { get; set; }
    }

    public class RequestAgentStateChange : BaseRequest
    {
        public string AgentId { get; set; }
        public AgentState State { get; set; }
    }
}