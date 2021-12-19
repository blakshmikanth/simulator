using System.Collections.Generic;

namespace Simulator.Engine.Directory
{
    public class Store: IStore
    {
        public Store()
        {
            Agents = new List<AgentResource>
            {
                new AgentResource("A1", "Agent", "01", "1001"),
                new AgentResource("A2", "Agent", "02", "1002"),
                new AgentResource("A3", "Agent", "03", "1003")
            };
        }
        
        public IList<AgentResource> Agents { get;  }
    }
}