using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Common.Validators.Courses;
using GoCode.Application.Courses.Commands;
using GoCode.UnitTests.Attributes.Customization;
using System.Diagnostics.CodeAnalysis;
using GoCode.UnitTests.Attributes;
using GoCode.Application.Common.Constants;

namespace GoCode.UnitTests.Application.Common.Validators
{
    [ExcludeFromCodeCoverage]
    public class CreateCourseCommandValidatorTests
    {
        private static IEnumerable<object[]> GetValidCourses()
        {
            yield return new object[]
            {
                new CreateCourseCommand
                {
                    Name = "Course 1",
                    Description = "Description",
                    Questions = new List<CreateQuestionDto>
                    {
                        new CreateQuestionDto
                        {
                            Content = "Question1",
                            XP = 5
                        },
                        new CreateQuestionDto
                        {
                            Content = "Question2",
                            XP = 5
                        }
                    }
                }
            };
        }

        private readonly IValidator<CreateCourseCommand> _sut;
        private readonly Mock<IValidator<CreateQuestionDto>> _questionValidator;

        public CreateCourseCommandValidatorTests()
        {
            _questionValidator = new();
            _questionValidator.Setup(x => x.Validate(It.IsAny<CreateQuestionDto>()))
                .Returns(new ValidationResult());
            _sut = new CreateCourseCommandValidator(_questionValidator.Object);
        }

        [Theory]
        [MemberData(nameof(GetValidCourses))]
        public void Validate_GivenCorrectAnswear_ShouldNotReturnErrrors(CreateCourseCommand createCourseCommand)
        {
            //Act
            var result = _sut.TestValidate(createCourseCommand);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1001)]
        [InlineData(2000)]
        public void Validate_GivenCreateCourseCommand_WhenNameHasIncorrectLength_ShouldReturnErrror(int charCount)
        {
            //Arrange
            var fixture = new Fixture();
            var customizedFixture = fixture.Customize(new CorrectCourseCustomization());
            var command = customizedFixture.Create<CreateCourseCommand>();
            command.Name = new string('a', charCount);

            //Act
            var result = _sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2001)]
        [InlineData(3000)]
        public void Validate_GivenCreateCourseCommand_WhenDescriptionHasIncorrectLength_ShouldReturnErrror(int charCount)
        {
            //Arrange
            var fixture = new Fixture();
            var customizedFixture = fixture.Customize(new CorrectCourseCustomization());
            var command = customizedFixture.Create<CreateCourseCommand>();
            command.Description = new string('a', charCount);

            //Act
            var result = _sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Theory]
        [CorrectCourse]
        public void Validate_GivenCreateCourseCommand_WhenQuestionsAreDuplicated_ShouldReturnErrror(CreateCourseCommand createCourseCommand)
        {
            //Arrange
            createCourseCommand.Questions = new List<CreateQuestionDto>
            {
                new CreateQuestionDto
                {
                    Content = "Question1",
                    XP = 5
                },
                new CreateQuestionDto
                {
                    Content = "Question1",
                    XP = 5
                }
            };

            //Act
            var result = _sut.TestValidate(createCourseCommand);

            //Assert
            result.ShouldHaveAnyValidationError()
                .WithErrorMessage(ErrorMessages.Question.QuestionsMustBeUnique);
        }
    }
}
