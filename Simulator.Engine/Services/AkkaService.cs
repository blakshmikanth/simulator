using System;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DependencyInjection;
using Akka.Dispatch.SysMsg;
using Microsoft.Extensions.Hosting;
using Simulator.Engine.Actors;

namespace Simulator.Engine.Services
{
    public class AkkaService: IHostedService, IProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private ActorSystem _actorSystem;

        public AkkaService(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime)
        {
            _serviceProvider = serviceProvider;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        /// <summary>
        /// Root actor system
        /// </summary>
        public ActorSystem Root => _actorSystem;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var bootstrap = BootstrapSetup.Create();
            
            //enable DI support inside the ActorSystem, if needed
            var diSetup = DependencyResolverSetup.Create(_serviceProvider);
            
            //merge this setup (and any others) together into ActorSystemSetup
            var actorSystemSetup = bootstrap.And(diSetup);
            
            _actorSystem = ActorSystem.Create("Simulator", actorSystemSetup);
            
            CreateRootActors();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Create root actors
        /// </summary>
        private void CreateRootActors()
        {
            SystemActors.Logger = CreateActor<LoggerActor>("logger");
            SystemActors.Agents = CreateActor<AgentsActor>("agents");
        }
        
        private IActorRef CreateActor<T>(string name) where T : ReceiveActor
        {
            var props = DependencyResolver.For(_actorSystem).Props<T>();
            return _actorSystem.ActorOf(props, name);
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _actorSystem.Terminate();
            return Task.CompletedTask;
        }
    }
}