using AutoMapper;
using GoCode.Application.Common;
using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Courses.Queries;
using GoCode.Application.Courses.Responses;
using GoCode.Domain.Entities;

namespace GoCode.Application.Courses.Handlers
{
    public class GetAllCoursesInfoQueryHandler : IHandlerWrapper<GetAllCoursesInfoQuery, GetAllCoursesInfoResponse>
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetAllCoursesInfoQueryHandler(
            ICoursesRepository coursesRepository,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _coursesRepository = coursesRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public Task<Response<GetAllCoursesInfoResponse>> Handle(GetAllCoursesInfoQuery request, CancellationToken cancellationToken)
        {
            var courses = _coursesRepository.GetCoursesWithAll().ToList();
            var mappedCourses = _mapper.Map<IEnumerable<CourseInfoDto>>(courses).ToList();
            SetIsUserSignedUpForCourse(courses, mappedCourses);
            var response = new GetAllCoursesInfoResponse() { Courses = mappedCourses };
            return Task.FromResult(ResponseResult.Ok(response));
        }

        private void SetIsUserSignedUpForCourse(List<Course> courses, List<CourseInfoDto> courseInfos)
        {
            for (int i = 0; i < courses.Count(); i++)
            {
                var isSignedUp = courses[i].UserCourses.Any(uc => uc.UserId == _currentUserService?.User?.Id);
                courseInfos[i].IsUserSignedUp = isSignedUp;
            }
        }
    }
}
