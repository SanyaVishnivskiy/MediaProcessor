using AutoMapper;
using Core.Business.Auth.Component;
using Core.Business.Auth.Models;
using Core.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserComponent _component;
        private readonly IMapper _mapper;

        public UsersController(IUserComponent component, IMapper mapper)
        {
            _component = component ?? throw new ArgumentNullException(nameof(component));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mapped = _mapper.Map<CreateUserModel>(user);
            var result = await _component.Create(mapped);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return StatusCode(204);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Invalid id " + id);

            if (!IsAllowedToSee(id))
            {
                return StatusCode(403, "You don't have permissions to see this user");
            }

            var result = await _component.GetById(id);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDTO>(result));
        }

        private bool IsAllowedToSee(string id)
        {
            return User.IsInRole("admin")
                 || id == User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        [HttpGet]
        [Route("employee/{id}")]
        public async Task<IActionResult> GetByName(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Invalid id " + id);

            var result = await _component.GetById(id);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDTO>(result));
        }

        [HttpPut]
        [Authorize(Roles="admin")]
        [Route("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UpdateUserDTO dto)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Invalid id " + id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dto.Id = id;
            var user = _mapper.Map<UpdateUserModel>(dto);
            var result = await _component.Update(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}
