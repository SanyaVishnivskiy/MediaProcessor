using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Concurrent;

namespace Scheduler.Configuration
{
    public class InjectableJobFactory : IJobFactory
    {
        private readonly IServiceProvider _provider;
        private ConcurrentDictionary<IJob, IServiceScope> _scopes = new ConcurrentDictionary<IJob, IServiceScope>();

        public InjectableJobFactory(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobType = bundle.JobDetail.JobType;
            var scope = _provider.CreateScope();
            try
            {
                var job = CreateJob(scope.ServiceProvider, jobType);
                _scopes.AddOrUpdate(job, _ => scope, (_, __) => scope);
                return job;
            }
            catch (Exception e)
            {
                scope.Dispose();
                return null;
            }
        }

        private IJob CreateJob(IServiceProvider serviceProvider, Type jobType)
        {
            var job = (IJob)serviceProvider.GetService(jobType);
            if (job is null)
                throw new Exception("Can not resolve job");

            return job;
        }

        public void ReturnJob(IJob job)
        {
            _scopes.TryRemove(job, out _);
        }
    }
}
