// ****************************************
// FileName:Program.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-15
// Revision History:
// ****************************************

using System;
using Microsoft.Owin.Hosting;

namespace TeamFight.Host
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";
            WebApp.Start<Startup>(baseAddress);
            Console.WriteLine("WebApp服务器启动成功 URL:" + baseAddress);

            Console.ReadLine();
        }
    }
}