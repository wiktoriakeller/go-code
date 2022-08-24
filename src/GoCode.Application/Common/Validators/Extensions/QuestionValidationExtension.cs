using FluentValidation;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Questions.Commands;

namespace GoCode.Application.Common.Validators.Extensions
{
    public static class QuestionValidationExtension
    {
        public static IRuleBuilderOptions<T, CreateQuestionCommand> CreateQuestionCommandContentMustBeUnique<T>(this IRuleBuilder<T, CreateQuestionCommand> ruleBuilder,
            ICoursesRepository coursesRepository)
        {
            return ruleBuilder.MustAsync(async (rootObject, command, context) =>
            {
                return await IsContentUnique(coursesRepository, command.CourseId, command.Question.Content);
            })
            .WithMessage(ErrorMessages.Question.QuestionsMustBeUnique);
        }

        public static IRuleBuilderOptions<T, UpdateQuestionCommand> UpdateQuestionCommandContentMustBeUnique<T>(this IRuleBuilder<T, UpdateQuestionCommand> ruleBuilder,
            ICoursesRepository coursesRepository, IQuestionsRepository questionsRepository)
        {
            return ruleBuilder.MustAsync(async (rootObject, command, context) =>
            {
                var question = await questionsRepository.FirstOrDefaultAsyncWith(x => x.Id == command.Id, x => x.Course);

                if (question is null)
                {
                    return false;
                }

                return await IsContentUnique(coursesRepository, question.CourseId, command.Question.Content, question.Id);
            })
            .WithMessage(ErrorMessages.Question.QuestionsMustBeUnique);
        }

        private static async Task<bool> IsContentUnique(ICoursesRepository coursesRepository, int courseId, string commandContent, int? questionId = null)
        {
            var course = await coursesRepository.FirstOrDefaultWithAllAsync(x => x.Id == courseId);

            if (course is null)
            {
                return false;
            }

            var questions = course.Questions.Select(x => new { x.Id, x.Content });
            var contentsSet = new HashSet<string>();

            foreach (var question in questions)
            {
                if (question.Content == commandContent && (questionId is null || questionId != question.Id))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
