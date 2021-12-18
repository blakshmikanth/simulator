using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DependencyInjection;
using Simulator.Engine.Actors;
using Simulator.Engine.Directory;
using Simulator.Engine.Services;

namespace Simulator.Engine
{
    public class Seeder
    {
        private readonly IStore _store;

        public Seeder(IStore store)
        {
            _store = store;
        }
        
        public async Task SeedAsync()
        {
            // Create new agent actors
            foreach (var agent in _store.Agents)
            {
                SystemActors.Agents.Tell(AgentsActor.CreateAgentCreatedMessage(agent));    
            }
            await Task.CompletedTask;
        }

    }
}