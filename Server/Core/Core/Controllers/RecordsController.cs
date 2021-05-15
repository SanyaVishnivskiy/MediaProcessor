using AutoMapper;
using Core.Business.Files.Component.Models;
using Core.Business.Records.Component;
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
        private readonly IRecordsComponent _component;
        private readonly IFilesComponent _filesComponent;
        private readonly IActionsProcessorFacade _actionsFacade;
        private readonly IMapper _mapper;

        public RecordsController(
            IRecordsComponent component,
            IFilesComponent filesComponent,
            IActionsProcessorFacade actionsFacade,
            IMapper mapper)
        {
            _component = component ?? throw new ArgumentNullException(nameof(component));
            _filesComponent = filesComponent ?? throw new ArgumentNullException(nameof(filesComponent));
            _actionsFacade = actionsFacade ?? throw new ArgumentNullException(nameof(actionsFacade));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]Pagination pagination)
        {
            var result = await _component.GetWithDependencies(pagination);
            var mappedItems = result.RecreateWithType(x => _mapper.Map<List<RecordDTO>>(x));
            return Ok(mappedItems);
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

        [HttpGet("{recordId}/download")]
        public async Task<IActionResult> GetBlobDownload(string recordId)
        {
            var record = await _component.GetById(recordId);
            var stream = await _filesComponent.Download(record.File);
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
