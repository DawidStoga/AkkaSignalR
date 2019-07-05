using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DBActor;
using Akka.Message;
using Common.Actor;

namespace Akka.DB.Actor.Actor
{
    public class DbManagerActor : ReceiveActor
    {
        IActorRef originalSender, DbActor;
        Random rnd = new Random();
        public DbManagerActor()
        {
            Init();
        }
        public void Init()
        {
            Guid id = Guid.Parse("15e17855-32c4-46b5-95b8-02ba9ab0d6bd");
            var props = Props.Create<DbActor>(id, Self);
            DbActor = Program.DBActorSystem.ActorOf(props, "DbActor");
            Receive<GetUsersMsg>(m => PreAction(m));
        }
        private void Start()
        {
            originalSender = Sender;
            Receive<Msg>(m => Action(m));
            Receive<ResponseMsg>(m => ResponseAction(m));
        }
        private void Action(Msg msg)
        {
            if (msg.Id == 0 && msg.MsgType != MsgType.GET)
            {
                int userid = rnd.Next(1, 99999);
                msg.Id = userid;
            }
            DbActor.Forward(msg);
        }
        private void ResponseAction(ResponseMsg msg)
        {
            Console.WriteLine("Responded count"+ msg.Msgs.Count());
            originalSender.Forward(msg);
        }
        private void PreAction(Msg msg)
        {
            Console.WriteLine("user Data msgtype " + msg.MsgType + ", user: " + msg.Name + " , " + DateTime.Now.ToString("dd-mm-yy HH:mm ss"));
            DbActor.Tell(msg);
            Become(Start);
        }
    }
    public class YellowActor : UntypedActor
    {
        private const string ActorName = "YellowActor";
        private const ConsoleColor MessageColor = ConsoleColor.Yellow;

        private IActorRef _greenActor;

        protected override void PreStart()
        {
            base.PreStart();

            _greenActor = Context.ActorOf<GreenActor>();
        }

        protected override void OnReceive(object message)
        {
            if (message is string)
            {
                var msg = message as string;

                PrintMessage(msg);
                _greenActor.Tell(msg);
            }
        }

        private void PrintMessage(string message)
        {
            Console.ForegroundColor = MessageColor;
            Console.WriteLine(
                "{0} on thread #{1}: {2}",
                ActorName,
                Thread.CurrentThread.ManagedThreadId,
                message);
        }
    }
    public class GreenActor : ReceiveActor
    {
        private const string ActorName = "GreenActor";
        private const ConsoleColor MessageColor = ConsoleColor.Green;
        private const ConsoleColor ResponseColor = ConsoleColor.DarkGreen;

        private IActorRef _blueActor;

        protected override void PreStart()
        {
            base.PreStart();

            var lastActorProps = Props.Create<BlueActor>();
            _blueActor = Context.ActorOf(lastActorProps);

            Become(HandleString);
        }

        private void HandleString()
        {
            Receive<string>(s =>
            {
                PrintMessage(s);
                _blueActor.Tell(s);
            });

            Receive<MessageReceived>(m => PrintResponse(m));
        }

        private void PrintResponse(MessageReceived message)
        {
            Console.ForegroundColor = ResponseColor;
            Console.Write("{0} on thread #{1}: ",
                ActorName,
                Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Receive message with counter: {0}",
                message.Counter);
        }
        private void PrintMessage(string message)
        {
            Console.ForegroundColor = MessageColor;
            Console.WriteLine(
                "{0} on thread #{1}: {2}",
                ActorName,
                Thread.CurrentThread.ManagedThreadId,
                message);
        }
    }
    public class MessageReceived
    {
        public int Counter { get; private set; }

        public MessageReceived(int counter)
        {
            Counter = counter;
        }
    }

    public class BlueActor : ReceiveActor
    {
        private const string ActorName = "BlueActor";
        private const ConsoleColor MessageColor = ConsoleColor.Blue;

        private int _counter = 0;

        protected override void PreStart()
        {
            base.PreStart();
            Become(HandleString);
        }

        private void HandleString()
        {
            Receive<string>(s =>
            {
                PrintMessage(s);
                _counter++;
                Sender.Tell(new MessageReceived(_counter));
            });
        }

        private void PrintMessage(string message)
        {
            Console.ForegroundColor = MessageColor;
            Console.WriteLine(
                "{0} on thread #{1}: {2}",
                ActorName,
                Thread.CurrentThread.ManagedThreadId,
                message);
        }
    }
}
