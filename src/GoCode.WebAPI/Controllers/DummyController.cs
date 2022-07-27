using GoCode.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using GoCode.Domain.Entities;

namespace GoCode.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public DummyController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpGet("GetToken")]
        public IActionResult GetToken()
        {
            var user = new User
            {
                Id = 1,
                Name = "Jhon",
                Surname = "Smith",
                Email = "jhon@smith.com"
            };

            return Ok(_jwtService.CreateJwtToken(user));
        }
    }
}