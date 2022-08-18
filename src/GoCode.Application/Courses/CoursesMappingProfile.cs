using AutoMapper;
using GoCode.Application.Courses.Commands;
using GoCode.Application.Courses.Requests;

namespace GoCode.Application.Courses
{
    internal class CoursesMappingProfile : Profile
    {
        public CoursesMappingProfile()
        {
            CreateMap<CreateCourseRequest, CreateCourseCommand>();
        }
    }
}
