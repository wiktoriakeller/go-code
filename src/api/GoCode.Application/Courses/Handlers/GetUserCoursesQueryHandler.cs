using AutoMapper;
using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Courses.Queries;
using GoCode.Application.Courses.Responses;
using MediatR;

namespace GoCode.Application.Courses.Handlers
{
    public class GetUserCoursesQueryHandler : IRequestHandler<GetUserCoursesQuery, Response<GetUserCoursesResponse>>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly ICoursesRepository _coursesRepository;
        private readonly IMapper _mapper;

        public GetUserCoursesQueryHandler(ICurrentUserService currentUser,
            ICoursesRepository coursesRepository,
            IMapper mapper)
        {
            _currentUser = currentUser;
            _coursesRepository = coursesRepository;
            _mapper = mapper;
        }

        public async Task<Response<GetUserCoursesResponse>> Handle(GetUserCoursesQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _currentUser.User;

            if (currentUser is null)
            {
                return ResponseResult.Unauthorized<GetUserCoursesResponse>(ErrorMessages.Identity.UnauthorizedUser);
            }

            var courses = _coursesRepository.GetCoursesWithAll(x => x.UserCourses.Any(uc => uc.UserId == currentUser.Id));
            var mappedCourses = _mapper.Map<IEnumerable<CourseDto>>(courses);
            var response = new GetUserCoursesResponse() { Courses = mappedCourses };
            return ResponseResult.Ok(response);
        }
    }
}
