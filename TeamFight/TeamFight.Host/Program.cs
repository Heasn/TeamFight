using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace TeamFight.Host
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";
            WebApp.Start<Startup>(baseAddress);
            Console.WriteLine("WebApp服务器启动成功 URL:"+baseAddress);

            HttpClient client = new HttpClient();

            HttpContent b = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("","1")
            });
            
           
            var response = client.PostAsync(baseAddress + "api/account/login",b).Result;

            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.ReadLine();

            Console.ReadLine();
        }
    }
}