using GoCode.Application.Courses.Commands;
using GoCode.Application.Courses.Queries;
using GoCode.Application.Courses.Requests;
using Microsoft.AspNetCore.Authorization;

namespace GoCode.WebAPI.Controllers
{
    [Authorize]
    [Route("api/v1/courses")]
    public class CoursesController : BaseApiController
    {
        public CoursesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        /// <summary>
        /// Gets all available courses info
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllCoursesInfo()
        {
            var query = new GetAllCoursesInfoQuery();
            var response = await _medaitor.Send(query);
            return GetStatusCode(response);
        }

        /// <summary>
        /// Gets courses for a logged in user
        /// </summary>
        /// <returns></returns>
        [HttpGet("users")]
        public async Task<IActionResult> GetUserCourses()
        {
            var query = new GetUserCoursesQuery();
            var response = await _medaitor.Send(query);
            return GetStatusCode(response);
        }

        /// <summary>
        /// Sign up for a course with specific ID
        /// </summary>
        [HttpPatch("users/{id}")]
        public async Task<IActionResult> SignUpForACourse([FromRoute] int id)
        {
            var command = new SignUpForCourseCommand() { Id = id };
            var response = await _medaitor.Send(command);
            return GetStatusCode(response);
        }

        /// <summary>
        /// Create course, only for admin
        /// </summary>
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

        /// <summary>
        /// Update course, only for admin
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCourse([FromRoute] int id, [FromBody] UpdateCourseRequest request)
        {
            var command = _mapper.Map<UpdateCourseCommand>(request);
            command.Id = id;
            var response = await _medaitor.Send(command);
            return GetStatusCode(response);
        }

        /// <summary>
        /// Delete course, only for admin
        /// </summary>
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
