using System.Collections.Generic;

namespace Processor.Resources
{
    public interface IEnvironmentContext
    {
        IList<Agent> Agents { get; set; }
    }
}