using Newtonsoft.Json.Linq;
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
                .UsingJobData(CreateData(data.Data))
                .StartNow()
                .Build();
        }

        private JobDataMap CreateData(JObject data)
        {
            var map = new Dictionary<string, object>
            {
                { "Data", data.ToString() }
            };

            return new JobDataMap((IDictionary<string, object>)map);
        }
    }
}
