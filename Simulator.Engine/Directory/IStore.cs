using System.Collections.Generic;

namespace Simulator.Engine.Directory
{
    public interface IStore
    {
        public IList<AgentResource> Agents { get;  }
    }
}