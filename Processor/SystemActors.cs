using Akka.Actor;

namespace Processor
{
    public static class SystemActors
    {
        public static IActorRef AgentsActor = ActorRefs.Nobody;
    }
}