using AutoMapper;
using GoCode.Application.Common.Dtos;
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
            CreateMap<CreateCourseCommand, Course>();
            CreateMap<Course, CreateCourseResponse>();
            CreateMap<CreateQuestionDto, Question>()
                .ReverseMap();
            CreateMap<CreateAnswearDto, Answear>()
                .ReverseMap();
            CreateMap<Course, CourseDto>();
            CreateMap<Question, QuestionDto>();
            CreateMap<Answear, AnswearDto>();
        }
    }
}
