using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Scheduler.Component;
using Scheduler.Jobs;

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
            services.AddTransient<ActionsJob>();

            services
                .AddTransient<ITriggerBuilderFactory, TriggerBuilderFactory>();

            services.AddTransient<IJobsComponent, JobsComponent>();
        }

        private static void ComposeQuartzScheduler(IServiceCollection services, SchedulerConfiguration schedulerConfiguration)
        {
            services.AddSingleton(p => {
                var configuration = new QuartzSchedulerConfiguration();
                var scheduler = configuration.ConfigureScheduler(p, schedulerConfiguration);

                scheduler.AddJob(CreateJob(JobType.Actions), true)
                    .GetAwaiter()
                    .GetResult();

                return scheduler;
            });
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
