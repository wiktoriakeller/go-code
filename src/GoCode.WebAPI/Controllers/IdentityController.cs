using GoCode.Application.Contracts.Identity;
using GoCode.Application.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GoCode.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            var response = await _identityService.CreateUserAsync(request);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Created($"api/identity/{response.Result.UserId}", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserRequest request)
        {
            var response = await _identityService.AuthenticateUserAync(request);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}