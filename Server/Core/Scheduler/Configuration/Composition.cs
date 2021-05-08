using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Scheduler.Component;
using Scheduler.Jobs;
using System;

namespace Scheduler.Configuration
{
    public static class Composition
    {
        private static readonly JobsMappings _mappings = new JobsMappings();

        public static IServiceCollection ComposeScheduler(this IServiceCollection services, IConfiguration configuration)
        {
            var schedulerConfiguration = new SchedulerConfiguration();
            configuration.GetSection("Scheduler")
                .Bind(schedulerConfiguration);

            ComposeQuartzScheduler(services, schedulerConfiguration);
            ComposeJobs(services);

            return services;
        }

        private static void ComposeJobs(IServiceCollection services)
        {
            services
                .AddTransient<ITriggerBuilderFactory, TriggerBuilderFactory>();

            services.AddTransient<IJobsComponent, JobsComponent>();
        }

        private static void ComposeQuartzScheduler(IServiceCollection services, SchedulerConfiguration schedulerConfiguration)
        {
            var configuration = new QuartzSchedulerConfiguration();
            var scheduler = configuration.ConfigureScheduler(schedulerConfiguration);
            services.AddSingleton(scheduler);

            scheduler.AddJob(CreateJob(JobType.Actions), false)
                .GetAwaiter()
                .GetResult();

            scheduler.Start().GetAwaiter().GetResult();
        }

        private static IJobDetail CreateJob(JobType type)
        {
            (string job, string group) = _mappings.GetJobIdentity(type);
            return JobBuilder.Create<ActionsJob>()
                .WithIdentity(job, group)
                .StoreDurably(true)
                .Build();
        }
    }
}
