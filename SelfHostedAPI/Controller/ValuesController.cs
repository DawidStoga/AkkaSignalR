using System.Linq;
using System.Web.Http;
using Akka.Actor;
using Akka.Message;
using SelfHostedAPI.Actor;

namespace SelfHostedAPI.Controller
{
    public class ValuesController:ApiController
    {
        [Route("get")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            SystemActors.UserActor.Tell(new GetUsersMsg { } as Msg);
            return Ok();
        }
        [Route("post")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]AddUserMsg user)
        {
            SystemActors.UserActor.Tell(user as Msg);
            return Ok();
        }
        [Route("put")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]UpdateUserMsg user)
        {
            SystemActors.UserActor.Tell(user as Msg);
            return Ok();
        }
        [Route("delete")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            SystemActors.UserActor.Tell(new DeleteUserMsg { Id = id } as Msg);
            return Ok();
        }
    }
}
