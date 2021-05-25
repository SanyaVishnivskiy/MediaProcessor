using AutoMapper;
using Core.Business.Auth.Component;
using Core.Business.Models;
using Core.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthComponent _component;
        private readonly IMapper _mapper;

        public AuthController(IAuthComponent component, IMapper mapper)
        {
            _component = component ?? throw new ArgumentNullException(nameof(component));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _component.Login(_mapper.Map<LoginModel>(login));
            if (!result.Succeeded)
            {
                return BadRequest("Credentials are not correct");
            }

            return Ok(new
            {
                token = result.Token,
                employeeId = login.EmployeeId
            });
        }
    }
}
