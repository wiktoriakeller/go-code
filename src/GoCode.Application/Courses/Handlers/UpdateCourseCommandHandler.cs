using AutoMapper;
using GoCode.Application.Common;
using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Courses.Commands;
using GoCode.Application.Courses.Responses;
using GoCode.Domain.Entities;

namespace GoCode.Application.Courses.Handlers
{
    public class UpdateCourseCommandHandler : IHandlerWrapper<UpdateCourseCommand, UpdateCourseResponse>
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IMapper _mapper;

        public UpdateCourseCommandHandler(ICoursesRepository coursesRepository,
            IMapper mapper)
        {
            _coursesRepository = coursesRepository;
            _mapper = mapper;
        }

        public async Task<Response<UpdateCourseResponse>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _coursesRepository.FirstOrDefaultWithAllAsync(x => x.Id == request.Id);
            course.Name = request.Course.Name;
            course.Description = request.Course.Description;
            course.XP = request.Course.XP;
            course.PassPercentTreshold = request.Course.PassPercentTreshold;
            course.Questions = _mapper.Map<ICollection<Question>>(request.Course.Questions);

            var updatedCourse = await _coursesRepository.UpdateAsync(course);
            var response = _mapper.Map<UpdateCourseResponse>(updatedCourse);
            return ResponseResult.Updated(response);
        }
    }
}
