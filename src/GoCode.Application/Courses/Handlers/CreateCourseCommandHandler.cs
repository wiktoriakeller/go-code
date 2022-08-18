using GoCode.Application.Common;
using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Courses.Commands;
using GoCode.Application.Courses.Responses;

namespace GoCode.Application.Courses.Handlers
{
    public class CreateCourseCommandHandler : IHandlerWrapper<CreateCourseCommand, CreateCourseResponse>
    {
        public Task<Response<CreateCourseResponse>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
