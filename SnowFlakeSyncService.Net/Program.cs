using Geckoboard;
using geckoboardcsharp;
using geckoboardcsharp.Models;
using Hangfire;
using Hangfire.SQLite;
using Microsoft.Owin.Hosting;
using Snowflake.Data.Client;
using SnowFlakeSyncService.Net.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace SnowFlakeSyncService.Net
{
    class Program
    {
        static void Main(string[] args)
        {

            const string url = "http://localhost:8000";
            using (WebApp.Start<Startup>(url))
            {

                Console.WriteLine("Server started at:" + url);
                Console.ReadLine();
            }
        }
    }
}
