using FluentValidation;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Common.Validators.Extensions;
using GoCode.Application.Courses.Commands;

namespace GoCode.Application.Common.Validators.Courses
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator(
            IValidator<CreateCourseDto> courseValidator,
            ICoursesRepository coursesRepository)
        {
            RuleFor(x => x.Id)
                .EntityWithIdMustExist(coursesRepository)
                .WithMessage(string.Format(ErrorMessages.NotFound, "Course"));

            RuleFor(x => x.Course)
                .SetValidator(courseValidator);
        }
    }
}
