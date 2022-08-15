using AutoMapper;
using GoCode.Application.Courses.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoCode.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/courses")]
    public class CoursesController : BaseApiController
    {
        public CoursesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetUserCourses()
        {
            var query = new GetUserCoursesQuery();
            var response = await _medaitor.Send(query);
            return StatusCode((int)response.HttpStatusCode, response);
        }
    }
}
