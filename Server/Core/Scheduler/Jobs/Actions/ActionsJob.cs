using Core.Business.Auth;
using FileProcessor.Actions;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Scheduler.Jobs
{
    public class ActionsJob : IJob
    {
        private readonly IActionsProcessorFacade _processor;
        private readonly IJsonActionsParser _parser;
        private readonly IMutableCurrentUser _user;
        private readonly ILogger<ActionsJob> _logger;

        public ActionsJob(
            IActionsProcessorFacade processor,
            IJsonActionsParser parser,
            IMutableCurrentUser user,
            ILogger<ActionsJob> logger)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var json = context.MergedJobDataMap.GetString("Data");
            InitUser(context.MergedJobDataMap);
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

        private void InitUser(JobDataMap jobDataMap)
        {
            var list = new List<Claim>
            {
                new Claim(ClaimTypes.Name, jobDataMap.GetString("CreatedBy"))
            };
            _user.SetClaims(list);
        }
    }
}
