using AutoMapper;
using Core.Business.Records.Facade;
using Core.Business.Records.Models;
using Core.Common.Models;
using Core.Models;
using FileProcessor.Actions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordsComponentFacade _facade;
        private readonly IActionsProcessorFacade _actionsFacade;
        private readonly IMapper _mapper;
        private readonly JwtBearerHandler _handler;

        public RecordsController(
            IRecordsComponentFacade facade,
            IActionsProcessorFacade actionsFacade,
            IMapper mapper,
            JwtBearerHandler handler)
        {
            _facade = facade ?? throw new ArgumentNullException(nameof(facade));
            _actionsFacade = actionsFacade ?? throw new ArgumentNullException(nameof(actionsFacade));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
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
            if (result == null)
            {
                return NotFound();
            }

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
        [AllowAnonymous]
        public async Task<IActionResult> GetBlobDownload(string recordId, [FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
                return StatusCode(401, "Not authorized");
            var result = CheckCanAccessFile(token);
            if (result is null)
                return StatusCode(403, "Unauthorized");

            var record = await _facade.GetById(recordId);
            var stream = await _facade.Download(record);
            var contentType = "APPLICATION/octet-stream";
            var fileName = record.FileName;
            return File(stream, contentType, fileName);
        }

        private async Task<bool> CheckCanAccessFile(string token)
        {
            var result = await _handler.AuthenticateAsync();
            if (!result.Succeeded)
            {
                return false;
            }

            if (result.Principal.IsInRole("admin")) 
            {
                return true;
            }

            // TODO Check has access
            if (result.Principal.IsInRole("employee"))
            {
                return true;
            }

            return false;
        }

        [HttpGet("{recordId}/actions")]
        public async Task<IActionResult> GetActions(string recordId)
        {
            var actions = await _actionsFacade.GetActions(recordId);
            return Ok(actions);
        }
    }
}
