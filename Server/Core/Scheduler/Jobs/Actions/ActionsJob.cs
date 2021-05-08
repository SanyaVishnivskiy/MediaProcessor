using Quartz;
using System.Threading.Tasks;

namespace Scheduler.Jobs
{
    public class ActionsJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var json = context.MergedJobDataMap.GetString("Data");
            return Task.CompletedTask;
        }
    }
}
