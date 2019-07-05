using System;
using SelfHostedAPI.Actor;
using Microsoft.Owin.Hosting;
using System.Net.Http;
using System.Threading;
using System.Configuration;
using System.Net;
using System.Net.Sockets;

namespace SelfHostedAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9876/";
            SetupActorSystem.Start();
            string actorSystemName = ConfigurationManager.AppSettings["actorSystemName"];
            Console.Title = actorSystemName;
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Web API Self hosted and running on ::" + baseAddress + "...");
                var client = new HttpClient();
                Console.WriteLine("Running test request on ::" + baseAddress + "...");
                string geturl = baseAddress + "hello";
                var response = client.GetAsync(geturl).Result;
                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine("Test ran successful... on ::" + geturl + "...");
                Console.WriteLine();
                Thread.Sleep(Timeout.Infinite);
            }
        }
    }
}
