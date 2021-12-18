namespace Simulator.Engine.Contracts
{
    public class RequestAgentLogout : BaseRequest
    {
        public string AgentId { get; set; }
    }
}