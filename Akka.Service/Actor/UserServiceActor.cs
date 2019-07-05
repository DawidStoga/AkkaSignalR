using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Message;

namespace Common.Actor
{
    public class UserServiceActor : ReceiveActor
    {
        string UserDataActorPath = "akka.tcp://DBActorSystem@localhost:5249/user/UserDataActor";
        public UserServiceActor()
        {
            Receive<Msg>(msg => ActionUserReceiveHandler(msg));
        }
        private void ActionUserReceiveHandler(Msg msg)
        {
            Console.WriteLine("User service actor called @ to " + msg.MsgType+ " user:->" + DateTime.Now.ToString("dd-mm-yy HH:mm ss"));
            var dataActor=Context.ActorSelection(UserDataActorPath).ResolveOne(TimeSpan.FromSeconds(10)).Result;
            dataActor.Forward(msg);
        }
    }
}