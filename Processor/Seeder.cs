using Akka.Actor;
using Processor.Resources;

namespace Processor
{
    public class Seeder
    {
        private readonly IEnvironmentContext _environmentContext;

        public Seeder(IEnvironmentContext environmentContext)
        {
            _environmentContext = environmentContext;
        }
        
        public void Seed()
        {
            // create all agents
            foreach (var agents in _environmentContext.Agents)
            {
                SystemActors.AgentsActor.Tell(agents, ActorRefs.Nobody);
            }
        }
    }
}