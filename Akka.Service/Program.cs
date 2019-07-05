using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Common.Actor;

namespace Akka.Service.Actor
{
    class Program
    {
        private static ActorSystem ServiceActorSystem;
        static void Main(string[] args)
        {
            string actorSystemName = ConfigurationManager.AppSettings["actorSystemName"];
            Console.Title = actorSystemName;

            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                ServiceActorSystem = ActorSystem.Create("ServiceActorSystem");
                Console.WriteLine("Service actor system started...");
                ServiceActorSystem.ActorOf<UserServiceActor>("UserServiceActor");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
