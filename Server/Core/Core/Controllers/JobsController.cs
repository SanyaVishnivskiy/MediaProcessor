using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Scheduler;
using Scheduler.Component;
using System;
using System.Threading.Tasks;

namespace Core.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IJobsComponent _component;
        private readonly IMapper _mapper;

        public JobsController(IJobsComponent component, IMapper mapper)
        {
            _component = component ?? throw new ArgumentNullException(nameof(component));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]JobDataDTO data)
        {
            var jobData = new JobData
            {
                Data = JObject.Parse(data.Data.ToString()),
                Type = (JobType)Enum.Parse(typeof(JobType), data.Type),
                Id = data.Id
            };

            await _component.Create(jobData);
            return Ok();
        }
    }
}
