using System;
using Processor.Models;

namespace Processor.Contracts
{
    public class RequestUpdateAgentState
    {
        public RequestUpdateAgentState()
        {
            RequestId = Guid.NewGuid().ToString();
            Timestamp = (DateTime.Now - DateTime.UnixEpoch).TotalSeconds;
        }

        public RequestUpdateAgentState(string agentId, AgentStateEnum state): base()
        {
            AgentId = agentId;
            State = state;
        }
        
        public string RequestId { get; set; }
        public double Timestamp { get; set; }
        
        public string AgentId { get; set; }
        public AgentStateEnum State { get; set; }
    }
}