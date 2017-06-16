using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace TeamFight.WebApi
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";
            WebApp.Start<Startup>(baseAddress);
            Console.WriteLine("WebApp服务器启动成功 URL:"+baseAddress);
            Console.ReadLine();
        }
    }
}