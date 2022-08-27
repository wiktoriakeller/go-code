using AutoMapper;
using GoCode.Application.Courses.Commands;
using GoCode.Application.Courses.Requests;
using GoCode.Application.Courses.Responses;
using GoCode.Domain.Entities;

namespace GoCode.Application.Courses
{
    internal class CoursesMappingProfile : Profile
    {
        public CoursesMappingProfile()
        {
            CreateMap<CreateCourseRequest, CreateCourseCommand>();
            CreateMap<UpdateCourseRequest, UpdateCourseCommand>();
            CreateMap<CreateCourseCommand, Course>();
            CreateMap<Course, CreateCourseResponse>();
            CreateMap<Course, UpdateCourseResponse>();
        }
    }
}
