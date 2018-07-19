﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApplicationTest3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(GetConfigurationSources)
                .ConfigureLogging((hostingContext, logging) =>
                             {
                                 //logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                                 logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                                // logging.AddConsole();
                    //logging.AddDebug();
                    logging.SetMinimumLevel(LogLevel.Trace);
                             })
                .UseStartup<Startup>()
                .Build();

        private static void GetConfigurationSources(WebHostBuilderContext ctx, IConfigurationBuilder builder)
        {
            //removing the deafult configurtion options
            builder.Sources.Clear();

            builder.AddJsonFile("config.json", false, true)
                    .AddEnvironmentVariables();

        }
    }
}
