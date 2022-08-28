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
            CreateMap<CreateCourseRequest, CreateCourseCommand>()
                .BeforeMap((request, command) =>
                {
                    command.Course = new();
                    command.Course.Name = request.Name;
                    command.Course.Description = request.Description;
                    command.Course.XP = request.XP;
                    command.Course.PassPercentTreshold = request.PassPercentTreshold;
                    command.Course.Questions = request.Questions;
                });

            CreateMap<UpdateCourseRequest, UpdateCourseCommand>()
                .BeforeMap((request, command) =>
                {
                    command.Course = new();
                    command.Course.Name = request.Name;
                    command.Course.Description = request.Description;
                    command.Course.XP = request.XP;
                    command.Course.PassPercentTreshold = request.PassPercentTreshold;
                    command.Course.Questions = request.Questions;
                });

            CreateMap<CreateCourseDto, Course>();
            CreateMap<Course, CreateCourseResponse>();
            CreateMap<Course, UpdateCourseResponse>();
        }
    }
}
