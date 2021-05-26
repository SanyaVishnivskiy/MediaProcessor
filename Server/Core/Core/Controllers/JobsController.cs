using AutoMapper;
using Core.Business.Auth;
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
        private readonly ICurrentUser _user;
        private readonly IMapper _mapper;

        public JobsController(
            IJobsComponent component,
            ICurrentUser user,
            IMapper mapper)
        {
            _component = component ?? throw new ArgumentNullException(nameof(component));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]JobDataDTO data)
        {
            var jobData = new JobData
            {
                Data = JObject.Parse(data.Data.ToString()),
                Type = (JobType)Enum.Parse(typeof(JobType), data.Type),
                Id = data.Id,
                CreatedBy = _user.EmployeeId
            };

            await _component.Create(jobData);
            return Ok();
        }
    }
}
