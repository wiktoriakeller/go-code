using AutoMapper;
using GoCode.Application.Common;
using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Courses.Commands;
using GoCode.Application.Courses.Responses;
using GoCode.Domain.Entities;

namespace GoCode.Application.Courses.Handlers
{
    public class CreateCourseCommandHandler : IHandlerWrapper<CreateCourseCommand, CreateCourseResponse>
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IMapper _mapper;

        public CreateCourseCommandHandler(ICoursesRepository coursesRepository,
            IMapper mapper)
        {
            _coursesRepository = coursesRepository;
            _mapper = mapper;
        }

        public async Task<Response<CreateCourseResponse>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var courseToAdd = _mapper.Map<Course>(request);
            var course = await _coursesRepository.AddAsync(courseToAdd);
            var response = _mapper.Map<CreateCourseResponse>(course);
            return ResponseResult.Created(response);
        }
    }
}
