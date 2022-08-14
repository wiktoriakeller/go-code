using AutoMapper;
using GoCode.Application.Identity.Commands;
using GoCode.Application.Identity.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoCode.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/identity")]
    public class IdentityController : BaseApiController
    {
        public IdentityController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            var command = _mapper.Map<CreateUserCommand>(request);
            var response = await _medaitor.Send(command);

            if (!response.Succeeded)
            {
                return StatusCode((int)response.HttpStatusCode, response);
            }

            return Created($"api/v1/identity/{response.Data.UserId}", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserRequest request)
        {
            var command = _mapper.Map<AuthenticateUserCommand>(request);
            var response = await _medaitor.Send(command);
            return StatusCode((int)response.HttpStatusCode, response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var command = _mapper.Map<RefreshTokenCommand>(request);
            var response = await _medaitor.Send(command);
            return StatusCode((int)response.HttpStatusCode, response);
        }
    }
}
