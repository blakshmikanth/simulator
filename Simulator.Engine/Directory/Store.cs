using System.Collections.Generic;

namespace Simulator.Engine.Directory
{
    public interface IStore
    {
        public IList<AgentResource> Agents { get;  }
    }
    
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

    public class AgentResource
    {
        public AgentResource()
        {
            
        }

        public AgentResource(string id, string firstName, string lastName, string extension)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Extension = extension;
        }
        
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Extension { get; set; }
    }
}