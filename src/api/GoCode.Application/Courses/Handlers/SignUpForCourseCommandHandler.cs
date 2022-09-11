using GoCode.Application.Common;
using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Courses.Commands;
using GoCode.Application.Courses.Responses;
using GoCode.Domain.Entities;

namespace GoCode.Application.Courses.Handlers
{
    public class SignUpForCourseCommandHandler : IHandlerWrapper<SignUpForCourseCommand, SignUpForCourseResponse>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly ICoursesRepository _courseRepository;
        private readonly IUserCoursesRepository _userCoursesRepository;

        public SignUpForCourseCommandHandler(ICurrentUserService currentUser,
            ICoursesRepository coursesRepository,
            IUserCoursesRepository userCoursesRepository)
        {
            _currentUser = currentUser;
            _courseRepository = coursesRepository;
            _userCoursesRepository = userCoursesRepository;
        }

        public async Task<Response<SignUpForCourseResponse>> Handle(SignUpForCourseCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _currentUser.User;

            if (currentUser is null)
            {
                return ResponseResult.Unauthorized<SignUpForCourseResponse>(ErrorMessages.Identity.UnauthorizedUser);
            }

            var selectedCourse = await _courseRepository.FirstOrDefaultWithAllAsync(x => x.Id == request.Id);

            var userCourse = new UserCourse()
            {
                CourseId = selectedCourse.Id,
                UserId = currentUser.Id,
            };

            await _userCoursesRepository.AddAsync(userCourse);
            var response = new SignUpForCourseResponse() { Id = selectedCourse.Id };
            return ResponseResult.Ok(response);
        }
    }
}
