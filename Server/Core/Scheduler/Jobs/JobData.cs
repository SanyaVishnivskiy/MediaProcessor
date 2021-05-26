using Newtonsoft.Json.Linq;

namespace Scheduler
{
    public class JobData
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public JobType Type { get; set; }
        public JObject Data { get; set; }
    }

    public enum JobType
    {
        Unknown = 0,
        Actions = 1
    }
}
