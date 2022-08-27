using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Common.Validators.Courses;
using GoCode.Application.Courses.Commands;
using GoCode.UnitTests.Attributes.Customization;

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
                    XP = 5,
                    PassPercentTreshold = 50,
                    Questions = new List<CreateQuestionDto>
                    {
                        new CreateQuestionDto
                        {
                            Content = "Question1",
                        },
                        new CreateQuestionDto
                        {
                            Content = "Question2",
                        }
                    }
                }
            };
        }

        private readonly IValidator<CreateCourseCommand> _sut;
        private readonly Mock<IValidator<CreateQuestionDto>> _questionValidatorMock;

        public CreateCourseCommandValidatorTests()
        {
            _questionValidatorMock = new();
            _questionValidatorMock.Setup(x => x.Validate(It.IsAny<CreateQuestionDto>()))
                .Returns(new ValidationResult());
            _sut = new CreateCourseCommandValidator(_questionValidatorMock.Object);
        }

        [Theory]
        [MemberData(nameof(GetValidCourses))]
        public void Validate_GivenCreateCourseCommand_ShouldNotReturnErrrors(CreateCourseCommand createCourseCommand)
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
        public void Validate_GivenCreateCourseCommand_WhenNameHasIncorrectLength_ShouldReturnErrrorForName(int charCount)
        {
            //Arrange
            var command = GetCreateCourseCommandFixture();
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
        public void Validate_GivenCreateCourseCommand_WhenDescriptionHasIncorrectLength_ShouldReturnErrrorForDescription(int charCount)
        {
            //Arrange
            var command = GetCreateCourseCommandFixture();
            command.Description = new string('a', charCount);

            //Act
            var result = _sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(-200)]
        public void Validate_GivenCreateCourseCommand_WhenXPPointAreLessThanOrEqualToZero_ShouldReturnErrrorForXP(int XP)
        {
            //Arrange
            var createQuestionDto = GetCreateCourseCommandFixture();
            createQuestionDto.XP = XP;

            //Act
            var result = _sut.TestValidate(createQuestionDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.XP);
        }

        [Theory]
        [InlineData(-500)]
        [InlineData(-1)]
        [InlineData(101)]
        [InlineData(500)]
        public void Validate_GivenCreateCourseCommand_WhenPassPercentTresholdIsNotBetweenZeroAndOneHundred_ShouldReturnErrrorForXP(int treshold)
        {
            //Arrange
            var createQuestionDto = GetCreateCourseCommandFixture();
            createQuestionDto.PassPercentTreshold = treshold;

            //Act
            var result = _sut.TestValidate(createQuestionDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.PassPercentTreshold);
        }

        [Theory]
        [CorrectCourseCommand]
        public void Validate_GivenCreateCourseCommand_WhenQuestionsAreDuplicated_ShouldReturnErrror(CreateCourseCommand createCourseCommand)
        {
            //Arrange
            createCourseCommand.Questions = new List<CreateQuestionDto>
            {
                new CreateQuestionDto
                {
                    Content = "Question1",
                },
                new CreateQuestionDto
                {
                    Content = "Question1",
                }
            };

            //Act
            var result = _sut.TestValidate(createCourseCommand);

            //Assert
            result.ShouldHaveAnyValidationError()
                .WithErrorMessage(ErrorMessages.Question.QuestionsMustBeUnique);
        }

        private CreateCourseCommand GetCreateCourseCommandFixture()
        {
            var fixture = new Fixture().Customize(new CorrectCourseCommandCustomization());
            return fixture.Create<CreateCourseCommand>();
        }
    }
}
