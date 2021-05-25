using System;
using Core.Business.Auth.Initializer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Quartz;

namespace Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            InitializeDB(host.Services);
            RunScheduler(host.Services);

            host.Run();
        }

        private static void RunScheduler(IServiceProvider services)
        {
            var scheduler = (IScheduler)services.GetService(typeof(IScheduler));
            scheduler.Start().GetAwaiter().GetResult();
        }

        private static void InitializeDB(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var initializer = (AuthInitializer)scope.ServiceProvider.GetService(typeof(AuthInitializer));
                initializer.InitializeAsync().GetAwaiter().GetResult();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    })
                .UseNLog();
    }
}
