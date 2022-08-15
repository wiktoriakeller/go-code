using GoCode.Application.BaseResponse;
using GoCode.Application.Courses.Responses;
using MediatR;

namespace GoCode.Application.Courses.Queries
{
    public class GetUserCoursesQuery : IRequest<Response<GetUserCoursesResponse>>
    {
    }
}
