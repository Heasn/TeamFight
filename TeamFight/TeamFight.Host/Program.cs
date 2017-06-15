using System;
using Nancy;
using Nancy.Hosting.Self;

namespace TeamFight.Host
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var url = new Url("http://localhost:9955");
            var hostConfig = new HostConfiguration {UrlReservations = new UrlReservations {CreateAutomatically = true}};
            using (var host = new NancyHost(hostConfig, url))
            {
                host.Start();

                Console.WriteLine("WEBSERVER 已启动 URL：" + url);
                Console.ReadLine();
            }
        }
    }
}