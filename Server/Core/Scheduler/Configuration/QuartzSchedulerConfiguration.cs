using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;

namespace Scheduler.Configuration
{
    public class QuartzSchedulerConfiguration
    {
        public IScheduler ConfigureScheduler(IServiceProvider provider, SchedulerConfiguration configuration)
        {
            NameValueCollection properties = new NameValueCollection()
            {
                { "quartz.scheduler.instanceName", "DefaultScheduler"},
                { "quartz.scheduler.instanceId", "auto"},
                { "quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz"},
                { "quartz.jobStore.useProperties", "true"},
                { "quartz.jobStore.dataSource", "default"},
                { "uartz.jobStore.tablePrefix", "QRTZ_"},
                { "quartz.jobStore.lockHandler.type", "Quartz.Impl.AdoJobStore.UpdateLockRowSemaphore, Quartz"},
                { "quartz.dataSource.default.connectionString", configuration.ConnectionString},
                { "quartz.dataSource.default.provider", "SqlServer" },
                { "quartz.serializer.type", "json" },
                { "quartz.jobStore.clustered", "true" },
                { "quartz.jobStore.driverDelegateType", "Quartz.Impl.AdoJobStore.StdAdoDelegate, Quartz" },
            };

            // First we must get a reference to a scheduler
            var factory = new StdSchedulerFactory(properties);
            var scheduler = factory.GetScheduler().GetAwaiter().GetResult();
            scheduler.JobFactory = new InjectableJobFactory(provider);

            return scheduler;
        }
    }
}
