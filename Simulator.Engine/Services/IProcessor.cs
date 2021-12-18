using Akka.Actor;

namespace Simulator.Engine.Services
{
    public interface IProcessor
    {
        public ActorSystem Root { get;  }
    }
}