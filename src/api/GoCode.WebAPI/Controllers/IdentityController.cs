using GoCode.Application.Identity.Commands;
using GoCode.Application.Identity.Requests;

namespace GoCode.WebAPI.Controllers
{
    [Route("api/v1/users")]
    public class IdentityController : BaseApiController
    {
        public IdentityController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        /// <summary>
        /// Register new account
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            var command = _mapper.Map<CreateUserCommand>(request);
            var response = await _medaitor.Send(command);

            if (!response.Succeeded)
            {
                return GetStatusCode(response);
            }

            return Created($"api/v1/identity/{response.Data?.Id}", response);
        }

        /// <summary>
        /// Login user
        /// </summary>
        [HttpPost("tokens")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserRequest request)
        {
            var command = _mapper.Map<AuthenticateUserCommand>(request);
            var response = await _medaitor.Send(command);
            return GetStatusCode(response);
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        [HttpPost("refresh-tokens")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var command = _mapper.Map<RefreshTokenCommand>(request);
            var response = await _medaitor.Send(command);
            return GetStatusCode(response);
        }
    }
}
