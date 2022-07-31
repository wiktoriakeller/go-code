using GoCode.Application.Contracts.Identity;
using GoCode.Application.Dtos.Requests;
using GoCode.Application.Identity.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoCode.WebAPI.Controllers
{
    [Route("api/v1/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _medaitor;

        public IdentityController(IMediator mediator)
        {
            _medaitor = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            var command = new CreateUserCommand
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password
            };

            var response = await _medaitor.Send(command);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Created($"api/identity/{response.Result.UserId}", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserRequest request)
        {
            var command = new AuthenticateUserCommand
            {
                Email = request.Email,
                Password = request.Password
            };

            var response = await _medaitor.Send(command);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}