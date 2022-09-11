using GoCode.Application.Courses.Commands;
using GoCode.Application.Courses.Queries;
using GoCode.Application.Courses.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace GoCode.WebAPI.Controllers
{
    [Authorize]
    [Route("api/v1/courses")]
    public class CoursesController : BaseApiController
    {
        public CoursesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet]
        [EnableCors("mobile")]
        public async Task<IActionResult> GetAllCourses()
        {
            var query = new GetAllCoursesQuery();
            var response = await _medaitor.Send(query);
            return GetStatusCode(response);
        }

        [HttpGet("user")]
        [EnableCors("mobile")]
        public async Task<IActionResult> GetUserCourses()
        {
            var query = new GetUserCoursesQuery();
            var response = await _medaitor.Send(query);
            return GetStatusCode(response);
        }

        [HttpPatch("signup/{id}")]
        [EnableCors("mobile")]
        public async Task<IActionResult> SignUpForACourse([FromRoute] int id)
        {
            var command = new SignUpForCourseCommand() { Id = id };
            var response = await _medaitor.Send(command);
            return GetStatusCode(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request)
        {
            var command = _mapper.Map<CreateCourseCommand>(request);
            var response = await _medaitor.Send(command);

            if (!response.Succeeded)
            {
                return GetStatusCode(response);
            }

            return Created($"api/v1/courses/{response.Data?.Id}", response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCourse([FromRoute] int id, [FromBody] UpdateCourseRequest request)
        {
            var command = _mapper.Map<UpdateCourseCommand>(request);
            command.Id = id;
            var response = await _medaitor.Send(command);
            return GetStatusCode(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int id)
        {
            var command = new DeleteCourseCommand() { Id = id };
            var response = await _medaitor.Send(command);
            return GetStatusCode(response);
        }
    }
}
