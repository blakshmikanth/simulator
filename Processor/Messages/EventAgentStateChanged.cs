using Processor.Models;

namespace Processor.Messages
{
    public class EventAgentStateChanged
    {
        /// <summary>
        /// Agent Id
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Current Agent State
        /// </summary>
        public AgentStateEnum State { get; set; }
    }
}