using GoCode.Application.Common;
using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Courses.Commands;
using GoCode.Application.Courses.Responses;

namespace GoCode.Application.Courses.Handlers
{
    public class DeleteCourseCommandHandler : IHandlerWrapper<DeleteCourseCommand, DeleteCourseResponse>
    {
        private readonly ICoursesRepository _coursesRepository;

        public DeleteCourseCommandHandler(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public async Task<Response<DeleteCourseResponse>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _coursesRepository.GetByIdAsync(request.Id);
            await _coursesRepository.DeleteAsync(course);
            var response = new DeleteCourseResponse() { Id = request.Id };
            return ResponseResult.Deleted(response);
        }
    }
}
