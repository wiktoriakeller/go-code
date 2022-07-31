using MediatR;
using AutoMapper;
using GoCode.Application.Dtos.Requests;
using GoCode.Application.Identity.Commands;
using Microsoft.AspNetCore.Mvc;

namespace GoCode.WebAPI.Controllers
{
    [Route("api/v1/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _medaitor;
        private readonly IMapper _mapper;

        public IdentityController(IMediator mediator, IMapper mapper)
        {
            _medaitor = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            var command = _mapper.Map<CreateUserCommand>(request);
            var response = await _medaitor.Send(command);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Created($"api/v1/identity/{response.Result.UserId}", null);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserRequest request)
        {
            var command = _mapper.Map<AuthenticateUserCommand>(request);
            var response = await _medaitor.Send(command);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}