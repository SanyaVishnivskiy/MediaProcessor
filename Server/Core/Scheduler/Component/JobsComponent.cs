using Quartz;
using Scheduler.Jobs;
using System;
using System.Threading.Tasks;

namespace Scheduler.Component
{
    public interface IJobsComponent
    {
        Task Create(JobData data);
        Task Delete(JobType type, string id);
    }

    public class JobsComponent : IJobsComponent
    {
        private readonly IScheduler _scheduler;
        private readonly ITriggerBuilderFactory _triggerFactory;

        public JobsComponent(
            IScheduler scheduler,
            ITriggerBuilderFactory triggerFactory)
        {
            _scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
            _triggerFactory = triggerFactory ?? throw new ArgumentNullException(nameof(triggerFactory));
        }

        public async Task Create(JobData data)
        {
            data.Id ??= Guid.NewGuid().ToString();

            var trigger = _triggerFactory.Create(data);
            await _scheduler.ScheduleJob(trigger);
        }

        public Task Delete(JobType type, string id)
        {
            throw new NotImplementedException();
        }
    }
}
