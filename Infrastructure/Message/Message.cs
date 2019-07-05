using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akka.Message
{
    public abstract class Msg
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public abstract MsgType MsgType { get; }
    }
    public class MsgData
    {
        public List<Msg> Users { get; }
        public MsgData(List<Msg> users)
        {
            Users = users;
        }
    }
    public class ResponseMsg
    {
        public Msg Msg { get; set; }
        public List<Msg> Msgs { get; set; }
    }
    public class GetUsersMsg:Msg
    {
        public override MsgType MsgType
        {
            get
            {
                return MsgType.GET;
            }

        }
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

    }

    public class AddUserMsg:Msg
    {
        public override MsgType MsgType
        {
            get
            {
                return MsgType.ADD;
            }

        }
    }
    public class UpdateUserMsg:Msg
    {
        public override MsgType MsgType
        {
            get
            {
                return MsgType.UPDATE;
            }

        }

    }
    public class DeleteUserMsg : Msg
    {
        public override MsgType MsgType
        {
            get
            {
                return MsgType.DELETE;
            }

        }

    }
    public enum MsgType
    {
        GET,ADD,UPDATE,DELETE
    }
}
