using System.Collections.Generic;

namespace Scheduler.Jobs
{
    public class JobsMappings
    {
        private static readonly Dictionary<JobType, JobKeyMap> _mappings
            = new Dictionary<JobType, JobKeyMap>
            {
                { JobType.Actions, new JobKeyMap(nameof(ActionsJob), "MainGroup") }
            };

        public (string, string) GetJobIdentity(JobType type)
        {
            if (_mappings.TryGetValue(type, out var value))
            {
                return (value.JobKey, value.GroupKey);
            }

            return ("", "");
        }

        private class JobKeyMap
        {
            public string JobKey { get; set; }
            public string GroupKey { get; set; }

            public JobKeyMap(string jobKey, string groupKey)
            {
                JobKey = jobKey;
                GroupKey = groupKey;
            }
        }
    }
}
