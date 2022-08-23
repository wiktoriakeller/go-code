using FluentValidation;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Questions.Commands;

namespace GoCode.Application.Common.Validators.Extensions
{
    public static class QuestionValidationExtension
    {
        public static IRuleBuilderOptions<T, CreateQuestionCommand> QuestionContentMustBeUnique<T>(this IRuleBuilder<T, CreateQuestionCommand> ruleBuilder,
            ICoursesRepository coursesRepository)
        {
            return ruleBuilder.MustAsync(async (rootObject, command, context) =>
            {
                var course = await coursesRepository.FirstOrDefaultWithAllAsync(x => x.Id == command.CourseId);

                if (course is null)
                {
                    return false;
                }

                var questionContents = course.Questions.Select(x => x.Content);
                var contentsSet = new HashSet<string>();
                foreach (var content in questionContents)
                {
                    if (content == command.Question.Content)
                    {
                        return false;
                    }
                }

                return true;
            })
            .WithMessage(ErrorMessages.Question.QuestionsMustBeUnique);
        }
    }
}
