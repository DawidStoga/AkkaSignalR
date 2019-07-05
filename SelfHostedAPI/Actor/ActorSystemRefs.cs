using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Common.Actor;

namespace SelfHostedAPI.Actor
{
    public static class SystemActors
    {
        public static IActorRef SignalRActor = ActorRefs.Nobody;

        public static IActorRef UserActor = ActorRefs.Nobody;
    }

    public class ActorSystemRefs
    {
        public static ActorSystem actorSystem;
    }
    public static class SetupActorSystem
    {
        public static void Start()
        {
            ActorSystemRefs.actorSystem = ActorSystem.Create("actorSystem");
            SystemActors.UserActor = ActorSystemRefs.actorSystem.ActorOf(Props.Create(() => new UserActor(ActorPaths.ActorPath)));
        }
    }
    public static class ActorPaths
    {
        public static string ActorPath = "akka.tcp://ServiceActorSystem@localhost:5248/user/UserServiceActor";
    }
}
