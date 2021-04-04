using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace consumer_ui
{
    // Main program file for the application.
    public class Program
    {
        // Main method. Build and run program. 
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
 
        // Configure and setup this app using Startup.cs
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
