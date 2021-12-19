using System.Collections.Generic;

namespace Processor.Resources
{
    public class EnvironmentContext: IEnvironmentContext
    {
        public EnvironmentContext()
        {
            Agents = new List<Agent>()
            {
                new Agent() {Id = "A1", FirstName = "Agent", LastName = "01", Extension = "1001"},
                new Agent() {Id = "A2", FirstName = "Agent", LastName = "02", Extension = "1002"},
                new Agent() {Id = "A3", FirstName = "Agent", LastName = "03", Extension = "1003"},
            };
        }
        
        public IList<Agent> Agents { get; set; }
    }
}