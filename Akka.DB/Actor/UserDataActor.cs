using System;
using System.Collections.Generic;
using System.Threading;
using Akka.Actor;
using Akka.DBActor;
using Akka.Message;
using Akka.Persistence;
using System.Linq;

namespace Common.Actor
{
    public class UserDataActor : ReceiveActor
    {

        public UserDataActor()
        {
            Receive<Msg>(m => Handle(m));
        }
        private void Handle(Msg msg)
        {
            Program.DbManagerActor.Forward(msg);
        }
    }
    public class DbActor : ReceivePersistentActor
    {
        private static List<Msg> _users = new List<Msg> { new GetUsersMsg { Id=100, Name="Dbactor", Age=15 }};
        Dictionary<MsgType,Action<Msg>> _action = new Dictionary<MsgType, Action<Msg>>();
        private Guid _id; private static IActorRef _rendererActor;
        public override string PersistenceId { get { return this._id.ToString(); } }
        public DbActor(Guid id, IActorRef rendererActor) {
            _action.Add(MsgType.GET, get);
            _action.Add(MsgType.ADD, add);
            _action.Add(MsgType.UPDATE, update);
            _action.Add(MsgType.DELETE, delete);

            _id = id; _rendererActor = rendererActor;
            this.Recover<Msg>(RecoverAndHandle, null);
            this.Command<Msg>(PersistAndHandle, null);
        }

        public void RecoverAndHandle(Msg message) {
            Handle(message);
        }
        public void PersistAndHandle(Msg message) {
            Persist(message, persistedMessage => Handle(persistedMessage));
        }
        public Action<Msg> get = (msg) => {  };
        public Action<Msg> add = (msg) => { _users.Add(msg);};
        public Action<Msg> delete = (msg) => {
            var user = _users.Where(u => u.Id == msg.Id).FirstOrDefault();
            if (user != null){
                _users.Remove(user);
            }
        };
        public Action<Msg> update = (msg) => {
                var user = _users.Where(u => u.Id == msg.Id).FirstOrDefault();
                if (user != null) {
                    _users.First(u => u.Id == msg.Id).Name = msg.Name;
                    _users.First(u => u.Id == msg.Id).Age = msg.Age;
                }
        };
        public void Handle(Msg message) {
            _action[message.MsgType](message);
            if (((ActorCell)Context).NumberOfMessages == 0) {
                var msgx = new ResponseMsg { Msgs = _users };
                Console.WriteLine("publish Count:" + _users.Count());
                _rendererActor.Tell(msgx);
            }
        }
    }
}
