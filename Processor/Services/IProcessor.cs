using Akka.Actor;

namespace Processor.Services
{
    public interface IProcessor
    {
        public ActorSystem System { get; }
    }
}