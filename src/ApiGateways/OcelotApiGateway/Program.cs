using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((host,builder)=> {
                builder.AddJsonFile($"ocelot.{host.HostingEnvironment.EnvironmentName}.json", true, true);
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureLogging((context, builder) =>
            {
                builder.AddConfiguration(context.Configuration.GetSection("Logging"));
                builder.AddConsole();
                builder.AddDebug();
            });
    }
}
