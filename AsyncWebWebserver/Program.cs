using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AsyncWebWebserver {
    public class Program {
        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // Creating a webhost with the default settings
                .ConfigureWebHostDefaults(webBuilder => {
                    // We configure the webserver
                    webBuilder.Configure(app => {
                        // Such when http request comes in, it runs this method
                        app.Run(async context => {
                            // Simulate Bad access of DB on the server side
                            // Thread.Sleep is basically blocking the webserver
                            // Akin to calling File.Read instead of ReadAsync
                            //Thread.Sleep(1000);

                            // This is equally bad, blocks the thread
                            //Task.Delay(1000).Wait();

                            // Simulate a Good access of DB as we no longer waiting
                            await Task.Delay(10000);

                            // Teacher tested this with apache benchmark,
                            // By sending hundred requests, result shows using await improved response time

                            await context.Response.WriteAsync("Hello World");
                        });
                    });

                });

    }
}
