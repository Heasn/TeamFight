using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TeamFight.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:9955/api/teamservice/create";
            if (Console.ReadLine() == "create")
            {
                WebClient wc = new WebClient();
                var encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                byte[] postData = encoding.GetBytes("{\"CharId\":\"1\"}");
                byte[] responseData = wc.UploadData(url, "POST", postData);
                Console.Write(encoding.GetString(responseData));             
            }
            Console.ReadLine();
        }
    }
}
