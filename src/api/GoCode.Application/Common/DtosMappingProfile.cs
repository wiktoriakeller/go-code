using AutoMapper;
using GoCode.Application.Common.Dtos;
using GoCode.Domain.Entities;

namespace GoCode.Application.Common
{
    internal class DtosMappingProfile : Profile
    {
        public DtosMappingProfile()
        {
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
