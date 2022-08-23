using AutoMapper;
using GoCode.Application.Questions.Commands;
using GoCode.Application.Questions.Requests;

namespace GoCode.Application.Questions
{
    internal class QuestionMappingProfile : Profile
    {
        public QuestionMappingProfile()
        {
            CreateMap<CreateQuestionRequest, CreateQuestionCommand>();
        }
    }
}
