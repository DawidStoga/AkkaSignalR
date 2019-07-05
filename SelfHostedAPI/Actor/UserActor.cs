using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Message;
using System.Linq;
using Microsoft.AspNet.SignalR;
using SelfHostedAPI.Controller;

namespace Common.Actor
{
    public class UserActor : ReceiveActor
    {
        int totalCalls = 0;
        public string UserServiceActorPath { get; set; }
        public UserActor(string userServiceActorPath)
        {
            UserServiceActorPath = userServiceActorPath;
            Receive<Msg>(msg => ActionUserReceiveHandler(msg));
            Receive<ResponseMsg>(msg => ActionResponseUserReceiveHandler(msg));
        }
        private void ActionUserReceiveHandler(Msg msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("User actor called @ to "+msg.MsgType +" user:" + msg.Name + "->" + DateTime.Now.ToString("dd-mm-yy HH:mm ss"));
            Context.ActorSelection(UserServiceActorPath).Tell(msg,Self);
        }
        private void ActionResponseUserReceiveHandler(ResponseMsg msg)
        {
            Console.WriteLine("resp call count -> "+ msg.Msgs.Count() + ", " + DateTime.Now.ToString("dd-mm-yy HH:mm ss"));
            Functions.Send(msg.Msgs);
            totalCalls++;
        }
    }
    public class Functions
    {
        public static void Send(List<Msg> msg)
        {
            var users = msg.Select(m => new User {
                 Age=m.Age,
                  Id=m.Id,
                  Name=m.Name
            });
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            hubContext.Clients.All.sendData(users);
        }
    }
}
