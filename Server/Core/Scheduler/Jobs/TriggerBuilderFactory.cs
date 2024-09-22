using Quartz;
using System.Collections.Generic;

namespace Scheduler.Jobs
{
    public interface ITriggerBuilderFactory
    {
        ITrigger Create(JobData data);
    }

    public class TriggerBuilderFactory : ITriggerBuilderFactory
    {
        private static readonly JobsMappings _mappings = new JobsMappings();

        public ITrigger Create(JobData data)
        {
            (string jobKey, string groupKey) = _mappings.GetJobIdentity(data.Type);

            return TriggerBuilder.Create()
                .WithIdentity(data.Id, groupKey)
                .ForJob(new JobKey(jobKey, groupKey))
                .UsingJobData(CreateData(data))
                .StartNow()
                .Build();
        }

        private JobDataMap CreateData(JobData data)
        {
            var map = new Dictionary<string, object>
            {
                { "Data", data.Data.ToString() },
                { "CreatedBy", data.CreatedBy }
            };

            return new JobDataMap((IDictionary<string, object>)map);
        }
    }
}
