using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Quartz.Spi;
using Scheduler.Jobs;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Configuration
{
    public class QuartzSchedulerConfiguration
    {
        public IScheduler ConfigureScheduler(SchedulerConfiguration configuration)
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
            scheduler.JobFactory = new JobFactory();

            //LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());
            return scheduler;
        }

        //private class ConsoleLogProvider : ILogProvider
        //{
        //    public Logger GetLogger(string name)
        //    {
        //        return (level, func, exception, parameters) =>
        //        {
        //            if ((level == LogLevel.Info || level == LogLevel.Warn || level == LogLevel.Error) && func != null)
        //            {
        //                Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func(), parameters);
        //            }
        //            return true;
        //        };
        //    }

        //    public IDisposable OpenNestedContext(string message)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public IDisposable OpenMappedContext(string key, object value, bool destructure = false)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }

    public class JobFactory : IJobFactory
    {
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var map = bundle.JobDetail.JobDataMap;
            return new ActionsJob();
        }

        public void ReturnJob(IJob job)
        {
            
        }
    }
}
