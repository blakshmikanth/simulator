using Akka.Actor;

namespace Simulator.Engine.Services
{
    public static class SystemActors
    {
        public static IActorRef Logger { get; set; }
        public static IActorRef Agents { get; set; }
    }
}