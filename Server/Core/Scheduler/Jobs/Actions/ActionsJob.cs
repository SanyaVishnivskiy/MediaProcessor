using FileProcessor.Actions;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Scheduler.Jobs
{
    public class ActionsJob : IJob
    {
        private readonly IActionsProcessorFacade _processor;
        private readonly IJsonActionsParser _parser;
        private readonly ILogger<ActionsJob> _logger;

        public ActionsJob(
            IActionsProcessorFacade processor,
            IJsonActionsParser parser,
            ILogger<ActionsJob> logger)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var json = context.MergedJobDataMap.GetString("Data");
            try
            {
                var request = _parser.Parse(json);

                await _processor.Process(request);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "An error occured on action job executing");
            }
        }
    }
}
