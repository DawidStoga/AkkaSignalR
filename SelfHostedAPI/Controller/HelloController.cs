using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SelfHostedAPI.Controller
{
    public class HelloController : ApiController
    {
        [Route("Hello")]
        public IHttpActionResult GetHello()
        {
            return Content(System.Net.HttpStatusCode.OK, "SelfHostedServer");
        }
        [Route("api/get")]
        // GET api/values 
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/get/{id}")]
        // GET api/values/5 
        public string Get(int id)
        {
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
        }
        [Route("api/post")]

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }
        [Route("api/delete")]

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
    [HubName("myHub")]
    public class MyHub : Hub
    {
        public override Task OnConnected()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            Console.WriteLine("ConnectedId : " +Context.ConnectionId);
            return base.OnConnected();

        }
        public void Send(string data)
        {
            ////IN ORDER TO INVOKE SIGNALR FUNCTIONALITY DIRECTLY FROM SERVER SIDE WE MUST USE THIS
            //var hubContext = GlobalHost.ConnectionManager.GetHubContext<MyHub>();

            ////PUSHING DATA TO ALL CLIENTS
            //hubContext.Clients.All.Add(data);


        }
    }
}
