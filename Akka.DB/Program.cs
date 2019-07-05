using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DB.Actor.Actor;
using Akka.Message;
using Common.Actor;

namespace Akka.DBActor
{
    public class Program
    {
        public static ActorSystem DBActorSystem;
        public static IActorRef DataActor,DbManagerActor;
        static void Main(string[] args)
        {
            string actorSystemName = ConfigurationManager.AppSettings["actorSystemName"];
            Console.Title = actorSystemName;

            try
            {
                DBActorSystem = ActorSystem.Create("DBActorSystem");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("DB actor system started on thread: {0}", Thread.CurrentThread.ManagedThreadId);
                DataActor=DBActorSystem.ActorOf<UserDataActor>("UserDataActor");
                DbManagerActor = DBActorSystem.ActorOf<DbManagerActor>("DbManagerActor");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
