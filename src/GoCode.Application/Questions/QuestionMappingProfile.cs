using AutoMapper;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Questions.Commands;
using GoCode.Application.Questions.Requests;
using GoCode.Application.Questions.Responses;
using GoCode.Domain.Entities;

namespace GoCode.Application.Questions
{
    internal class QuestionMappingProfile : Profile
    {
        public QuestionMappingProfile()
        {
            CreateMap<CreateQuestionRequest, CreateQuestionCommand>();

            CreateMap<CreateQuestionRequest, CreateQuestionCommand>()
                .BeforeMap((request, command) =>
                {
                    command.Question = new();
                    command.Question.Content = request.Content;
                    command.Question.XP = request.XP;
                    command.Question.Answers = request.Answers;
                });

            CreateMap<UpdateQuestionRequest, UpdateQuestionCommand>()
                .BeforeMap((request, command) =>
                {
                    command.Question = new();
                    command.Question.Content = request.Content;
                    command.Question.XP = request.XP;
                    command.Question.Answers = request.Answers;
                });

            CreateMap<Question, UpdateQuestionResponse>();
            CreateMap<Question, CreateQuestionResponse>();
        }
    }
}
