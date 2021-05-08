using Core.Business.Configuration;
using Core.Common.Models.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scheduler.Configuration;
using System.IO;

namespace Core.Configuration.DI
{
    public static class DiRegistrationsRoot
    {
        public static IServiceCollection RegisterDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            RegisterConfigurations(services, configuration);
            RegisterBusinessLayer(services);

            services.ComposeScheduler(configuration);

            return services;
        }

        private static void RegisterConfigurations(IServiceCollection services, IConfiguration configuration)
        {
            var localFileStoreOptions = new LocalFileStoreOptions();
            configuration
                .GetSection("FileStores:Local")
                .Bind(localFileStoreOptions);

            Directory.CreateDirectory(localFileStoreOptions.BaseFilePath);

            services.AddSingleton(localFileStoreOptions);
        }

        private static void RegisterBusinessLayer(IServiceCollection services)
        {
            var registrations = new DiBusinessRegistrations();
            registrations.Register(services);
        }
    }
}
