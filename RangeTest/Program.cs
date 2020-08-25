using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RangeTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    if (Environment.GetEnvironmentVariable("ServerName") == "Kestrel")
                    {
                        System.Console.WriteLine("Using Kestrel");
                        webBuilder.UseKestrel(options =>
                        {
                            options.Listen(IPAddress.Any, 5000);
                            options.Listen(IPAddress.Any, 5001,
                                listenOptions =>
                                {
                                    listenOptions.UseHttps();
                                }
                                );
                        });
                    }
                    webBuilder.UseStartup<Startup>();
                });
    }
}
