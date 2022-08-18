using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Courses.Queries;
using GoCode.Application.Courses.Responses;
using MediatR;

namespace GoCode.Application.Courses.Handlers
{
    public class GetUserCoursesHandler : IRequestHandler<GetUserCoursesQuery, Response<GetUserCoursesResponse>>
    {
        private readonly ICurrentUserService _currentUserService;

        public GetUserCoursesHandler(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public Task<Response<GetUserCoursesResponse>> Handle(GetUserCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = new List<CourseDto>
            {
                new CourseDto
                {
                    Id = 1,
                    Name = "Course"
                }
            };

            return Task.FromResult(ResponseResult.Ok(new GetUserCoursesResponse { Courses = courses }));
        }
    }
}
