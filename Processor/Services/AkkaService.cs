using System;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Processor.Actors;

namespace Processor.Services
{
    public class AkkaService : IHostedService, IProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private ActorSystem _actorSystem;

        public AkkaService(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime)
        {
            _serviceProvider = serviceProvider;
            _hostApplicationLifetime = hostApplicationLifetime;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            var bootstrap = BootstrapSetup.Create();

            // enable DI support inside the ActorSystem,if needed
            var diSetup = DependencyResolverSetup.Create(_serviceProvider);

            // merge this setup (and any others) together into ActorSystemSetup
            var actorSystemSetup = bootstrap.And(diSetup);

            _actorSystem = ActorSystem.Create("simulator", actorSystemSetup);
            
            var agentProps = DependencyResolver.For(_actorSystem).Props<AgentsActor>();
            SystemActors.AgentsActor = _actorSystem.ActorOf(agentProps, "agents");
            
            // add a continuation task that will guarantee shutdown of application if ActorSystem terminates
            _actorSystem.WhenTerminated.ContinueWith(tr => { _hostApplicationLifetime.StopApplication(); },
                cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _actorSystem.Terminate();
            return Task.CompletedTask;
        }

        public ActorSystem System => _actorSystem;
    }
}