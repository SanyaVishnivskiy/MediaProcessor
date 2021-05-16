using AutoMapper;
using Core.Business.Records.Facade;
using Core.Business.Records.Models;
using Core.Common.Models;
using Core.Models;
using FileProcessor.Actions;
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
        private readonly IRecordsComponentFacade _facade;
        private readonly IActionsProcessorFacade _actionsFacade;
        private readonly IMapper _mapper;

        public RecordsController(
            IRecordsComponentFacade facade,
            IActionsProcessorFacade actionsFacade,
            IMapper mapper)
        {
            _facade = facade ?? throw new ArgumentNullException(nameof(facade));
            _actionsFacade = actionsFacade ?? throw new ArgumentNullException(nameof(actionsFacade));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]Pagination pagination)
        {
            var result = await _facade.Get(pagination);
            var mappedItems = result.RecreateWithType(x => _mapper.Map<List<RecordDTO>>(x));
            return Ok(mappedItems);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var result = await _facade.GetById(id);
            return Ok(_mapper.Map<RecordDTO>(result));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody]RecordDTO record)
        {
            record.Id = id;
            var mapped = _mapper.Map<RecordModel>(record);
            await _facade.Update(mapped);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _facade.Delete(id);
            return Ok();
        }

        [HttpGet("{recordId}/download")]
        public async Task<IActionResult> GetBlobDownload(string recordId)
        {
            var record = await _facade.GetById(recordId);
            var stream = await _facade.Download(record);
            var contentType = "APPLICATION/octet-stream";
            var fileName = record.FileName;
            return File(stream, contentType, fileName);
        }

        [HttpGet("{recordId}/actions")]
        public async Task<IActionResult> GetActions(string recordId)
        {
            var actions = await _actionsFacade.GetActions(recordId);
            return Ok(actions);
        }
    }
}
