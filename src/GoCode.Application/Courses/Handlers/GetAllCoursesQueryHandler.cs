using AutoMapper;
using GoCode.Application.Common;
using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Courses.Queries;
using GoCode.Application.Courses.Responses;

namespace GoCode.Application.Courses.Handlers
{
    public class GetAllCoursesQueryHandler : IHandlerWrapper<GetAllCoursesQuery, GetAllCoursesResponse>
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IMapper _mapper;

        public GetAllCoursesQueryHandler(ICoursesRepository coursesRepository,
            IMapper mapper)
        {
            _coursesRepository = coursesRepository;
            _mapper = mapper;
        }

        public Task<Response<GetAllCoursesResponse>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = _coursesRepository.GetCoursesWithAll();
            var mappedCourses = _mapper.Map<IEnumerable<CourseDto>>(courses);
            var response = new GetAllCoursesResponse() { Courses = mappedCourses };
            return Task.FromResult(ResponseResult.Ok(response));
        }
    }
}
