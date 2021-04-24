using AutoMapper;
using Core.Business.Records.Component;
using Core.Business.Records.Models;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordsComponent _component;
        private readonly IMapper _mapper;

        public RecordsController(
            IRecordsComponent component,
            IMapper mapper)
        {
            _component = component;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _component.GetWithDependencies(null);
            return Ok(_mapper.Map<List<RecordDTO>>(result));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var result = await _component.GetById(id);
            return Ok(_mapper.Map<RecordDTO>(result));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody]RecordDTO record)
        {
            record.Id = id;
            var mapped = _mapper.Map<RecordModel>(record);
            await _component.Update(mapped);
            return Ok();
        }
    }
}
